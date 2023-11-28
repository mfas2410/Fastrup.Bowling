namespace Fastrup.Bowling.Domain.Model.Player;

public sealed class UserName
{
    private readonly string _value;

    public UserName(string? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value.Length > 30) throw new ArgumentException("Cannot exceed 30 characters", nameof(value));
        _value = value;
    }

    public override string ToString() => _value;

    public static implicit operator string(UserName obj) => obj._value;
}
