namespace Fastrup.Bowling.Domain.Model;

public sealed record Id
{
    private readonly Guid _value;

    public Id(Guid? value)
    {
        if (value is null) throw new ArgumentNullException(nameof(Id));
        if (value.Equals(Guid.Empty)) throw new ArgumentException("Cannot be empty", nameof(Id));
        _value = value.Value;
    }

    public override string ToString() => _value.ToString();

    public static implicit operator Guid(Id obj) => obj._value;
}
