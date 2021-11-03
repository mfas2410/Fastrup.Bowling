using Fastrup.Bowling.Domain.Abstractions;

namespace Fastrup.Bowling.Domain.Events
{
    public sealed class FrameCreatedEvent : IEvent
    {
        public FrameCreatedEvent(PinFrame frame) => NumberOfRolls = frame.NumberOfRolls;

        public int NumberOfRolls { get; }
    }
}