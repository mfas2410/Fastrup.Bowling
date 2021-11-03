﻿namespace Fastrup.Bowling.Domain.Abstractions
{
    public abstract record PinRoll
    {
        private readonly int _pinsKnockedOver;

        public int PinsKnockedOver
        {
            get => _pinsKnockedOver;
            init
            {
                ValidateRoll(value);
                _pinsKnockedOver = value;
            }
        }

        public abstract int PinsInLane { get; }

        protected abstract void ValidateRoll(int pinsKnockedOver);
    }
}