namespace Fastrup.Bowling.Domain.Model.Game;

public sealed class TenPinRoll : PinRoll
{
    public TenPinRoll(int pinsKnockedOver) : base(pinsKnockedOver) { }

    public override int PinsInLane => 10;

    protected override void ValidateRoll(int pinsKnockedOver)
    {
        if (pinsKnockedOver is < 0 or > 10) throw new RollException($"Invalid number of pins knocked over ({pinsKnockedOver}) - value must be between 0 and 10");
    }
}
