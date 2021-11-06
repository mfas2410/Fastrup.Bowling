using Fastrup.Bowling.Domain.Events;
using System.Collections.Generic;

namespace Fastrup.Bowling.Domain.Abstractions
{
    public abstract class PinScore
    {
        protected List<FrameCompletedEvent> FrameEvents = new();

        public abstract int Score { get; }

        public abstract void AddEvent(FrameCompletedEvent frameEvent);
    }
}