using Fastrup.Bowling.Domain.Abstractions;

namespace Fastrup.Bowling.Domain.Events
{
    public sealed class RollCreatedEvent : IEvent
    {
        public RollCreatedEvent(PinRoll roll)
        {
            PinsInLane = roll.PinsInLane;
            PinsKnockedOver = roll.PinsKnockedOver;
        }

        public int PinsInLane { get; }

        public int PinsKnockedOver { get; set; }
    }
}