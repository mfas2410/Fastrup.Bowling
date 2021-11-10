namespace Fastrup.Bowling.Domain.Model.Game;

public sealed class TenPinGame : PinGame
{
    private readonly IEventRegister _eventRegister;
    private readonly List<PinFrame> _frames = new();
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
        if (_frames.LastOrDefault() is null) _frames.Add(TenPinFrame.Create(Id, _playerIds[_currentPlayer], _frames.Count == _numberOfFrames - 1, _eventRegister));
        if (_frames.Last().IsComplete)
        {
            if (++_currentPlayer >= _playerIds.Length) _currentPlayer = 0;
            _frames.Add(TenPinFrame.Create(Id, _playerIds[_currentPlayer], _frames.Count == _numberOfFrames - 1, _eventRegister));
        }

        _frames.Last().AddRoll(pinRoll, _eventRegister);
        IsComplete = _frames.Count == _numberOfFrames * _playerIds.Length && _frames.Last().IsComplete;
        if (IsComplete) _eventRegister.RegisterEvent(new GameCompletedEvent(this));
    }

    public static TenPinGame Create(Id id, IEnumerable<Player.Player> players, IEventRegister eventRegister)
    {
        TenPinGame tenPinGame = new(id, players, eventRegister);
        eventRegister.RegisterEvent(new GameCreatedEvent(tenPinGame));
        return tenPinGame;
    }
}
