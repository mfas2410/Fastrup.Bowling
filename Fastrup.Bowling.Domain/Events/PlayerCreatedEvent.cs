namespace Fastrup.Bowling.Domain.Events;

public sealed class PlayerCreatedEvent : IEvent
{
    public PlayerCreatedEvent(Player player)
    {
        Id = player.Id.ToString();
        UserName = player.UserName;
    }

    public string Id { get; }
    public string UserName { get; }
}
