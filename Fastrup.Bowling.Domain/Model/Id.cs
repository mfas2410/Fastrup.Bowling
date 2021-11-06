using System;

namespace Fastrup.Bowling.Domain.Model
{
    public sealed record Id
    {
        private readonly Guid _value;

        public Id(Guid value) => Value = value;

        public Guid Value
        {
            get
            {
                if (_value.Equals(Guid.Empty)) throw new ArgumentException($"{nameof(Id)} not initialized");
                return _value;
            }
            init
            {
                if (value.Equals(Guid.Empty)) throw new ArgumentException($"{nameof(Id)} cannot be empty");
                _value = value;
            }
        }

        public override string ToString() => Value.ToString();
    }
}