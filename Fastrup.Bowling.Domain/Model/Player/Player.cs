using Fastrup.Bowling.Domain.Abstractions;
using Fastrup.Bowling.Domain.Events;

namespace Fastrup.Bowling.Domain.Model.Player
{
    public sealed record Player
    {
        private Player(Id id, UserName userName)
        {
            Id = id;
            UserName = userName;
        }

        public Id Id { get; }

        public UserName UserName { get; }

        public static Player Create(Id id, UserName userName, IEventRegister eventRegister)
        {
            var player = new Player(id, userName);
            eventRegister.RegisterEvent(new PlayerCreatedEvent(player));
            return player;
        }
    }
}