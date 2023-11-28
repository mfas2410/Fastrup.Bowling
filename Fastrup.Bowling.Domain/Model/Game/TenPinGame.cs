namespace Fastrup.Bowling.Domain.Model.Game;

public sealed class TenPinGame : PinGame
{
    private readonly IEventRegister _eventRegister;
    private readonly List<PinFrame> _frames = [];
    private readonly int _numberOfFrames = 10;
    private readonly Id[] _playerIds;
    private int _currentPlayer;

    private TenPinGame(Id id, IEnumerable<Player.Player> players, IEventRegister eventRegister) : base(id, players)
    {
        _playerIds = PlayerIds.ToArray();
        _eventRegister = eventRegister;
    }

    public override bool IsComplete { get; protected set; }

    public override void AddRoll(PinRoll pinRoll)
    {
        if (IsComplete) throw new GameException("The game has ended");
        if (_frames.LastOrDefault() is null) AddFrame();
        if (_frames.Last().IsComplete)
        {
            if (++_currentPlayer >= _playerIds.Length) _currentPlayer = 0;
            AddFrame();
        }

        _frames.Last().AddRoll(pinRoll);
        _eventRegister.RegisterEvent(new RollCreatedEvent(pinRoll));
        if (_frames.Last().IsComplete) _eventRegister.RegisterEvent(new FrameCompletedEvent(_frames.Last()));

        IsComplete = _frames.Count == _numberOfFrames * _playerIds.Length && _frames.Last().IsComplete;
        if (IsComplete) _eventRegister.RegisterEvent(new GameCompletedEvent(this));

        void AddFrame()
        {
            _frames.Add(new TenPinFrame(Id, _playerIds[_currentPlayer], _frames.Count == _numberOfFrames - 1));
            _eventRegister.RegisterEvent(new FrameCreatedEvent(_frames.Last()));
        }
    }

    public static TenPinGame Create(Id id, IEnumerable<Player.Player> players, IEventRegister eventRegister)
    {
        TenPinGame tenPinGame = new(id, players, eventRegister);
        eventRegister.RegisterEvent(new GameCreatedEvent(tenPinGame));
        return tenPinGame;
    }
}
