namespace Fastrup.Bowling.Domain.Events;

public sealed class GameCreatedEvent(PinGame game) : IEvent
{
    public string Id { get; } = game.Id.ToString();
    public IEnumerable<string> PlayerIds { get; } = game.PlayerIds.Select(x => x.ToString());
    public string Type { get; } = game.GetType().Name;
}
