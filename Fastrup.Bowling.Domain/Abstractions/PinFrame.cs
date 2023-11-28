namespace Fastrup.Bowling.Domain.Abstractions;

public abstract class PinFrame(Id gameId, Id playerId)
{
    protected readonly List<PinRoll> _rolls = [];

    public IReadOnlyCollection<PinRoll> Rolls => _rolls;

    public Id GameId { get; } = gameId;

    public Id PlayerId { get; } = playerId;

    public bool IsSpare => Rolls.Count >= NumberOfRolls && Rolls.Take(NumberOfRolls).Sum(x => x.PinsKnockedOver) == Rolls.FirstOrDefault()?.PinsInLane;

    public bool IsStrike => Rolls.FirstOrDefault()?.PinsKnockedOver == Rolls.FirstOrDefault()?.PinsInLane;

    public abstract bool IsComplete { get; protected set; }

    public abstract int NumberOfRolls { get; }

    public abstract void AddRoll(PinRoll roll);
}
