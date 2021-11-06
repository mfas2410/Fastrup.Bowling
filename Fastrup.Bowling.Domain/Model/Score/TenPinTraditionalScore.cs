using Fastrup.Bowling.Domain.Abstractions;
using Fastrup.Bowling.Domain.Events;
using System.Linq;

namespace Fastrup.Bowling.Domain.Model.Score
{
    public sealed class TenPinTraditionalScore : PinScore
    {
        public override int Score
        {
            get
            {
                int score = 0;
                FrameCompletedEvent[] events = FrameEvents.ToArray();
                for (int index = 0; index < events.Length; index++)
                {
                    FrameCompletedEvent frame = events[index];
                    if (frame.IsStrike || frame.IsSpare)
                    {
                        score += events[index..].SelectMany(x => x.Rolls).Take(3).Sum();
                    }
                    else
                    {
                        score += frame.Rolls.Sum();
                    }
                }

                return score;
            }
        }

        public override void AddEvent(FrameCompletedEvent frameEvent) => FrameEvents.Add(frameEvent);
    }
}