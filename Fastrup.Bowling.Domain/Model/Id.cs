namespace Fastrup.Bowling.Domain.Model;

public sealed record Id
{
    private readonly Guid _value;

    public Id(Guid? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value.Equals(Guid.Empty)) throw new ArgumentException("Cannot be empty", nameof(value));
        _value = value.Value;
    }

    public override string ToString() => _value.ToString();

    public static implicit operator Guid(Id obj) => obj._value;
}
