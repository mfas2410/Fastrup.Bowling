namespace Fastrup.Bowling.Domain.Abstractions;

public abstract class PinFrame
{
    protected List<PinRoll> _rolls = new();

    protected PinFrame(Id gameId, Id playerId)
    {
        GameId = gameId;
        PlayerId = playerId;
    }

    public IReadOnlyCollection<PinRoll> Rolls => _rolls;

    public Id GameId { get; }

    public Id PlayerId { get; }

    public bool IsSpare => Rolls.Count >= NumberOfRolls && Rolls.Take(NumberOfRolls).Sum(x => x.PinsKnockedOver) == Rolls.FirstOrDefault()?.PinsInLane;

    public bool IsStrike => Rolls.FirstOrDefault()?.PinsKnockedOver == Rolls.FirstOrDefault()?.PinsInLane;

    public abstract bool IsComplete { get; protected set; }

    public abstract int NumberOfRolls { get; }

    public abstract void AddRoll(PinRoll roll, IEventRegister eventRegister);
}
