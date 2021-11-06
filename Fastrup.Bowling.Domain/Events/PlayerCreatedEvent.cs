using Fastrup.Bowling.Domain.Abstractions;
using Fastrup.Bowling.Domain.Model.Player;

namespace Fastrup.Bowling.Domain.Events
{
    public sealed class PlayerCreatedEvent : IEvent
    {
        public PlayerCreatedEvent(Player player)
        {
            Id = player.Id.ToString();
            UserName = player.UserName.Value;
        }

        public string Id { get; }
        public string UserName { get; }
    }
}