using System;

namespace Fastrup.Bowling.Domain.Abstractions
{
    public abstract class PinGame
    {
        private Guid? _id;

        public Guid Id => _id ??= Guid.NewGuid();

        public abstract bool IsComplete { get; protected set; }

        public abstract void AddRoll(PinRoll pinRoll);
    }
}