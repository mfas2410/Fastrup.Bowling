namespace Fastrup.Bowling.Domain.Exceptions;

public sealed class RollException : Exception
{
    public RollException(string message) : base(message) { }
}
