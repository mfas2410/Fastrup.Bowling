using System;
using Fastrup.Bowling.Domain.Abstractions;

namespace Fastrup.Bowling.Domain.Events
{
    public sealed class GameCompletedEvent : IEvent
    {
        public GameCompletedEvent(PinGame game) => Id = game.Id;

        public Guid Id { get; }
    }
}