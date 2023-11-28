namespace Fastrup.Bowling.Domain.Events;

public sealed class FrameCreatedEvent(PinFrame frame) : IEvent
{
    public string GameId { get; } = frame.PlayerId.ToString();
    public string PlayerId { get; } = frame.PlayerId.ToString();
    public int NumberOfRolls { get; } = frame.NumberOfRolls;
}
