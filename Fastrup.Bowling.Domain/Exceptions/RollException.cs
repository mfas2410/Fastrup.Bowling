namespace Fastrup.Bowling.Domain.Exceptions;

public sealed class RollException(string message) : Exception(message);
