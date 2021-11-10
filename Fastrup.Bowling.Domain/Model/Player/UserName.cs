namespace Fastrup.Bowling.Domain.Model.Player;

public sealed record UserName
{
    private readonly string _value;

    public UserName(string? value)
    {
        if (value is null) throw new ArgumentNullException(nameof(UserName));
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Cannot be only whitespaces", nameof(UserName));
        if (value.Length > 30) throw new ArgumentException("Cannot exceed 30 characters", nameof(UserName));
        _value = value;
    }

    public override string ToString() => _value;

    public static implicit operator string(UserName obj) => obj._value;
}
