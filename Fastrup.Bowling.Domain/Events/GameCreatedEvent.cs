using System;
using Fastrup.Bowling.Domain.Abstractions;

namespace Fastrup.Bowling.Domain.Events
{
    public sealed class GameCreatedEvent : IEvent
    {
        public GameCreatedEvent(PinGame game) => Id = game.Id;

        public Guid Id { get; }
    }
}