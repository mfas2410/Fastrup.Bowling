namespace Fastrup.Bowling.Domain.Events;

public sealed class RollCreatedEvent(PinRoll roll) : IEvent
{
    public int PinsInLane { get; } = roll.PinsInLane;
    public int PinsKnockedOver { get; } = roll.PinsKnockedOver;
}
