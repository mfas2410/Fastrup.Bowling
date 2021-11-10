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
            Player player = new(id, userName);
            eventRegister.RegisterEvent(new PlayerCreatedEvent(player));
            return player;
        }
    }
}