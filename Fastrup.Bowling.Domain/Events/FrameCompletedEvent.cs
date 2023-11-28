namespace Fastrup.Bowling.Domain.Events;

public sealed class FrameCompletedEvent(PinFrame frame) : IEvent
{
    public string GameId { get; } = frame.PlayerId.ToString();
    public string PlayerId { get; } = frame.PlayerId.ToString();
    public bool IsSpare { get; } = frame.IsSpare;
    public bool IsStrike { get; } = frame.IsStrike;
    public IEnumerable<int> Rolls { get; } = frame.Rolls.Select(x => x.PinsKnockedOver);
}
