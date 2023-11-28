namespace Fastrup.Bowling.Domain.Model.Game;

public sealed class TenPinFrame(Id gameId, Id playerId, bool isLastFrame) : PinFrame(gameId, playerId)
{
    private int _numberOfBonusRolls;

    public override bool IsComplete { get; protected set; }

    public override int NumberOfRolls => 2;

    private bool NoMoreRollsAvailable => Rolls.Count == NumberOfRolls + _numberOfBonusRolls;

    private bool NotLastFrameAndIsStrike => !isLastFrame && IsStrike;

    public override void AddRoll(PinRoll pinRoll)
    {
        if (pinRoll is not TenPinRoll roll) throw new FrameException($"Only {nameof(TenPinRoll)} rolls allowed");
        if (IsComplete) throw new FrameException("The frame is completed");
        if (Rolls.Count > 0 && Rolls.Count < NumberOfRolls && !IsStrike && Rolls.First().PinsKnockedOver + roll.PinsKnockedOver > roll.PinsInLane) throw new FrameException("Invalid number of pins knocked over");

        _rolls.Add(roll);

        if (isLastFrame && (IsStrike || IsSpare)) _numberOfBonusRolls = 1;
        IsComplete = NotLastFrameAndIsStrike || NoMoreRollsAvailable;
    }
}
