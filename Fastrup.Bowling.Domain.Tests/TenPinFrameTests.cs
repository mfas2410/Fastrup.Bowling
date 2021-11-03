using System;
using Fastrup.Bowling.Domain.Events;
using Fastrup.Bowling.Domain.Exceptions;
using Fastrup.Bowling.Domain.Model.Game;
using FluentAssertions;
using Xunit;

namespace Fastrup.Bowling.Tests
{
    public sealed class TenPinFrameTests
    {
        private readonly EventRegister _eventRegister = new();

        [Fact]
        public void GivenAFrame_WhenKnockingAllPinsInFirstRoll_ThenItsAStrike()
        {
            // Arrange
            var sut = TenPinFrame.Create(false, _eventRegister);
            var roll = TenPinRoll.Create(10, _eventRegister);

            // Act
            sut.AddRoll(roll, _eventRegister);

            // Assert
            sut.IsComplete.Should().BeTrue();
            sut.IsSpare.Should().BeFalse();
            sut.IsStrike.Should().BeTrue();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is FrameCompletedEvent);
        }

        [Fact]
        public void GivenLastFrameAsStrike_WhenKnockingAllPinsInFirstRoll_ThenItsAStrikeWithTwoBonusRolls()
        {
            // Arrange
            var sut = TenPinFrame.Create(true, _eventRegister);
            var roll1 = TenPinRoll.Create(10, _eventRegister);
            var roll2 = TenPinRoll.Create(10, _eventRegister);
            var roll3 = TenPinRoll.Create(10, _eventRegister);

            // Act
            sut.AddRoll(roll1, _eventRegister);
            sut.AddRoll(roll2, _eventRegister);
            sut.AddRoll(roll3, _eventRegister);

            // Assert
            sut.IsComplete.Should().BeTrue();
            sut.IsSpare.Should().BeFalse();
            sut.IsStrike.Should().BeTrue();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is FrameCompletedEvent);
        }

        [Fact]
        public void GivenLastFrame_WhenKnockingAllPinsInFirstRoll_ThenItsAStrikeWithTwoBonusRolls()
        {
            // Arrange
            var sut = TenPinFrame.Create(true, _eventRegister);
            var roll1 = TenPinRoll.Create(10, _eventRegister);
            var roll2 = TenPinRoll.Create(3, _eventRegister);
            var roll3 = TenPinRoll.Create(4, _eventRegister);

            // Act
            sut.AddRoll(roll1, _eventRegister);
            sut.AddRoll(roll2, _eventRegister);
            sut.AddRoll(roll3, _eventRegister);

            // Assert
            sut.IsComplete.Should().BeTrue();
            sut.IsSpare.Should().BeFalse();
            sut.IsStrike.Should().BeTrue();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is FrameCompletedEvent);
        }

        [Fact]
        public void GivenLastFrameWithAllPinsKnockedOverInTwoRolls_WhenKnockingTooManyPinsInSecondRoll_ThrowException()
        {
            // Arrange
            var sut = TenPinFrame.Create(true, _eventRegister);
            var roll1 = TenPinRoll.Create(7, _eventRegister);
            var roll2 = TenPinRoll.Create(7, _eventRegister);

            // Act
            sut.AddRoll(roll1, _eventRegister);
            Action actual = () => sut.AddRoll(roll2, _eventRegister);

            // Assert
            actual.Should().ThrowExactly<FrameException>();
        }

        [Fact]
        public void GivenLastFrame_WhenKnockingAllPinsInBothRolls_ThenItsASpareWithOneBonusRoll()
        {
            // Arrange
            var sut = TenPinFrame.Create(true, _eventRegister);
            var roll1 = TenPinRoll.Create(4, _eventRegister);
            var roll2 = TenPinRoll.Create(6, _eventRegister);
            var roll3 = TenPinRoll.Create(7, _eventRegister);

            // Act
            sut.AddRoll(roll1, _eventRegister);
            sut.AddRoll(roll2, _eventRegister);
            sut.AddRoll(roll3, _eventRegister);

            // Assert
            sut.IsComplete.Should().BeTrue();
            sut.IsSpare.Should().BeTrue();
            sut.IsStrike.Should().BeFalse();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is FrameCompletedEvent);
        }

        [Fact]
        public void GivenACompletedFrame_WhenAddingRoll_ThenThrowException()
        {
            // Arrange
            var sut = TenPinFrame.Create(false, _eventRegister);
            sut.AddRoll(TenPinRoll.Create(10, _eventRegister), _eventRegister);
            var roll = TenPinRoll.Create(0, _eventRegister);

            // Act
            Action actual = () => sut.AddRoll(roll, _eventRegister);

            // Assert
            actual.Should().ThrowExactly<FrameException>();
        }

        [Fact]
        public void GivenAFrame_WhenKnockingAllPinsInTwoRolls_ThenItsASpare()
        {
            // Arrange
            var sut = TenPinFrame.Create(false, _eventRegister);
            var roll1 = TenPinRoll.Create(3, _eventRegister);
            var roll2 = TenPinRoll.Create(7, _eventRegister);

            // Act
            sut.AddRoll(roll1, _eventRegister);
            sut.AddRoll(roll2, _eventRegister);

            // Assert
            sut.IsComplete.Should().BeTrue();
            sut.IsSpare.Should().BeTrue();
            sut.IsStrike.Should().BeFalse();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is FrameCompletedEvent);
        }

        [Fact]
        public void GivenAFrame_WhenOnlyKnockingSomePinsInTwoRolls_ThenMarkFrameAsComplete()
        {
            // Arrange
            var sut = TenPinFrame.Create(false, _eventRegister);
            var roll1 = TenPinRoll.Create(3, _eventRegister);
            var roll2 = TenPinRoll.Create(4, _eventRegister);

            // Act
            sut.AddRoll(roll1, _eventRegister);
            sut.AddRoll(roll2, _eventRegister);

            // Assert
            sut.IsComplete.Should().BeTrue();
            sut.IsSpare.Should().BeFalse();
            sut.IsStrike.Should().BeFalse();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is FrameCompletedEvent);
        }
    }
}