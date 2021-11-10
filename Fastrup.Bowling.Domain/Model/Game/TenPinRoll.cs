namespace Fastrup.Bowling.Domain.Model.Game;

public sealed class TenPinRoll : PinRoll
{
    private TenPinRoll(int pinsKnockedOver) : base(pinsKnockedOver) { }

    public override int PinsInLane => 10;

    public static TenPinRoll Create(int pinsKnockedOver, IEventRegister eventRegister)
    {
        TenPinRoll roll = new(pinsKnockedOver);
        eventRegister.RegisterEvent(new RollCreatedEvent(roll));
        return roll;
    }

    protected override void ValidateRoll(int pinsKnockedOver)
    {
        if (pinsKnockedOver is < 0 or > 10) throw new RollException($"Invalid number of pins knocked over ({pinsKnockedOver}) - value must be between 0 and 10");
    }
}
