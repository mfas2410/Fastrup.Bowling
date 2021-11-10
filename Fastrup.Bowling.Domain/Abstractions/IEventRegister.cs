namespace Fastrup.Bowling.Domain.Abstractions;

public interface IEventRegister
{
    void RegisterEvent(IEvent domainEvent);
}
