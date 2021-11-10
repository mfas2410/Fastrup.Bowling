namespace Fastrup.Bowling.Domain.Events;

public sealed class GameCompletedEvent : IEvent
{
    public GameCompletedEvent(PinGame game)
    {
        Id = game.Id.ToString();
        PlayerIds = game.PlayerIds.Select(x => x.ToString());
        Type = game.GetType().Name;
    }

    public string Id { get; }
    public IEnumerable<string> PlayerIds { get; }
    public string Type { get; }
}
