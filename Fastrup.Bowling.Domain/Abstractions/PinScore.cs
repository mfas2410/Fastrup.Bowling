namespace Fastrup.Bowling.Domain.Abstractions;

public abstract class PinScore
{
    protected readonly List<FrameCompletedEvent> FrameEvents = [];

    public abstract int Score { get; }

    public abstract void AddEvent(FrameCompletedEvent frameEvent);
}
