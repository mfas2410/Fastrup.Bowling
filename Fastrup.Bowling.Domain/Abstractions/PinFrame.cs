using System.Collections.Generic;
using System.Linq;

namespace Fastrup.Bowling.Domain.Abstractions
{
    public abstract class PinFrame
    {
        public List<PinRoll> Rolls = new();

        public bool IsSpare => Rolls.Count >= NumberOfRolls && Rolls.Take(NumberOfRolls).Sum(x => x.PinsKnockedOver) == Rolls.FirstOrDefault()?.PinsInLane;

        public bool IsStrike => Rolls.FirstOrDefault()?.PinsKnockedOver == Rolls.FirstOrDefault()?.PinsInLane;

        public abstract bool IsComplete { get; protected set; }

        public abstract int NumberOfRolls { get; }

        public abstract void AddRoll(PinRoll roll, IEventRegister eventRegister);
    }
}