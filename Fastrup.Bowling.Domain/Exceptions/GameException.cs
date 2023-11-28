namespace Fastrup.Bowling.Domain.Exceptions;

public sealed class GameException(string message) : Exception(message);
