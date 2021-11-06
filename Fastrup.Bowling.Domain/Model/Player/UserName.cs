using System;

namespace Fastrup.Bowling.Domain.Model.Player
{
    public sealed record UserName
    {
        private readonly string? _value;

        public UserName(string value) => Value = value;

        public string Value
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_value)) throw new ArgumentException($"{nameof(UserName)} not initialized");
                return _value!;
            }

            init
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"{nameof(Value)} cannot be null or only whitespaces");
                if (value.Length > 30) throw new ArgumentException($"{nameof(Value)} cannot exceed 30 characters");
                _value = value;
            }
        }

        public override string ToString() => Value;
    }
}