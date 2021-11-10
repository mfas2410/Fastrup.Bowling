namespace Fastrup.Bowling.Domain.Events;

public sealed class FrameCreatedEvent : IEvent
{
    public FrameCreatedEvent(PinFrame frame)
    {
        GameId = frame.PlayerId.ToString();
        PlayerId = frame.PlayerId.ToString();
        NumberOfRolls = frame.NumberOfRolls;
    }

    public string GameId { get; }
    public string PlayerId { get; }
    public int NumberOfRolls { get; }
}
