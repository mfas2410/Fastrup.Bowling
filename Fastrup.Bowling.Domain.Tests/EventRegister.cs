using System.Collections.Generic;
using Fastrup.Bowling.Domain.Abstractions;

namespace Fastrup.Bowling.Tests
{
    public sealed class EventRegister : IEventRegister
    {
        public List<IEvent> DomainEvents { get; } = new();

        public void RegisterEvent(IEvent domainEvent) => DomainEvents.Add(domainEvent);
    }
}