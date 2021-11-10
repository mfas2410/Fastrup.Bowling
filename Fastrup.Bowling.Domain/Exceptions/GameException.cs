namespace Fastrup.Bowling.Domain.Exceptions;

public sealed class GameException : Exception
{
    public GameException(string message) : base(message) { }
}
