using System.Linq;
using Fastrup.Bowling.Domain.Abstractions;
using Fastrup.Bowling.Domain.Events;
using Fastrup.Bowling.Domain.Exceptions;

namespace Fastrup.Bowling.Domain.Model.Game
{
    public sealed class TenPinFrame : PinFrame
    {
        private readonly bool _isLastFrame;
        private int _numberOfBonusRolls;

        private TenPinFrame(bool isLastFrame) => _isLastFrame = isLastFrame;

        public override bool IsComplete { get; protected set; }

        public override int NumberOfRolls => 2;

        private bool NoMoreRollsAvailable => Rolls.Count == NumberOfRolls + _numberOfBonusRolls;

        private bool NotLastFrameAndIsStrike => !_isLastFrame && IsStrike;

        public override void AddRoll(PinRoll pinRoll, IEventRegister eventRegister)
        {
            if (pinRoll is not TenPinRoll roll) throw new FrameException($"Only {nameof(TenPinRoll)} rolls allowed");
            if (IsComplete) throw new FrameException("The frame is completed");
            if (Rolls.Count > 0 && Rolls.Count < NumberOfRolls && !IsStrike && Rolls.First().PinsKnockedOver + roll.PinsKnockedOver > roll.PinsInLane) throw new FrameException("Invalid number of pins knocked over");

            Rolls.Add(roll);

            if (_isLastFrame && (IsStrike || IsSpare)) _numberOfBonusRolls = 1;
            IsComplete = NotLastFrameAndIsStrike || NoMoreRollsAvailable;
            if (IsComplete) eventRegister.RegisterEvent(new FrameCompletedEvent(this));
        }

        public static TenPinFrame Create(bool isLastFrame, IEventRegister eventRegister)
        {
            var tenPinFrame = new TenPinFrame(isLastFrame);
            eventRegister.RegisterEvent(new FrameCreatedEvent(tenPinFrame));
            return tenPinFrame;
        }
    }
}