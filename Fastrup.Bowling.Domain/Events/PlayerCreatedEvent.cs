namespace Fastrup.Bowling.Domain.Events;

public sealed class PlayerCreatedEvent(Player player) : IEvent
{
    public string Id { get; } = player.Id.ToString();
    public string UserName { get; } = player.UserName;
}
