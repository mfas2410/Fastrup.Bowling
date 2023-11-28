namespace Fastrup.Bowling.Domain.Tests;

public sealed class EventRegister : IEventRegister
{
    public List<IEvent> DomainEvents { get; } = [];

    public void RegisterEvent(IEvent domainEvent) => DomainEvents.Add(domainEvent);
}
