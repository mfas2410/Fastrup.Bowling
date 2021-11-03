using System;

namespace Fastrup.Bowling.Domain.Exceptions
{
    public sealed class FrameException : Exception
    {
        public FrameException(string message) : base(message) { }
    }
}