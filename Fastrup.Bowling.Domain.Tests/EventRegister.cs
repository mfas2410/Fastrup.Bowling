namespace Fastrup.Bowling.Domain.Tests;

public sealed class EventRegister : IEventRegister
{
    public List<IEvent> DomainEvents { get; } = new();

    public void RegisterEvent(IEvent domainEvent) => DomainEvents.Add(domainEvent);
}
