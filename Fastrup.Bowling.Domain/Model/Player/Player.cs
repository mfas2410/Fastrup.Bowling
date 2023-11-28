namespace Fastrup.Bowling.Domain.Model.Player;

public sealed class Player(Id id, UserName userName)
{
    public Id Id { get; } = id;
    public UserName UserName { get; } = userName;

    public static Player Create(Id id, UserName userName, IEventRegister eventRegister)
    {
        Player player = new(id, userName);
        eventRegister.RegisterEvent(new PlayerCreatedEvent(player));
        return player;
    }
}
