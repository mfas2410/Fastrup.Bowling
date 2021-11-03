using System.Linq;
using Fastrup.Bowling.Domain.Abstractions;

namespace Fastrup.Bowling.Domain.Events
{
    public sealed class FrameCompletedEvent : IEvent
    {
        public FrameCompletedEvent(PinFrame frame)
        {
            IsSpare = frame.IsSpare;
            IsStrike = frame.IsStrike;
            Rolls = frame.Rolls.Select(x => x.PinsKnockedOver).ToArray();
        }

        public bool IsSpare { get; }

        public bool IsStrike { get; }

        public int[] Rolls { get; }
    }
}