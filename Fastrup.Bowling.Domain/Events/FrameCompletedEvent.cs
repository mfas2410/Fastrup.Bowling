namespace Fastrup.Bowling.Domain.Events;

public sealed class FrameCompletedEvent : IEvent
{
    public FrameCompletedEvent(PinFrame frame)
    {
        GameId = frame.PlayerId.ToString();
        PlayerId = frame.PlayerId.ToString();
        IsSpare = frame.IsSpare;
        IsStrike = frame.IsStrike;
        Rolls = frame.Rolls.Select(x => x.PinsKnockedOver);
    }

    public string GameId { get; }
    public string PlayerId { get; }
    public bool IsSpare { get; }
    public bool IsStrike { get; }
    public IEnumerable<int> Rolls { get; }
}
