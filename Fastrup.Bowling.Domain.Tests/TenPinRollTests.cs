using Fastrup.Bowling.Domain.Events;
using Fastrup.Bowling.Domain.Exceptions;
using Fastrup.Bowling.Domain.Model.Game;
using FluentAssertions;
using System;
using Xunit;

namespace Fastrup.Bowling.Domain.Tests
{
    public sealed class TenPinRollTests
    {
        private readonly EventRegister _eventRegister = new();

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GivenValidNumberOfPins_WhenInitializing_ThenReturnNumberOfPinsAndCreateEvent(int pinsKnockedOver)
        {
            // Arrange

            // Act
            var actual = TenPinRoll.Create(pinsKnockedOver, _eventRegister);

            // Assert
            actual.PinsKnockedOver.Should().Be(pinsKnockedOver);
            _eventRegister.DomainEvents.Should().HaveCount(1).And.AllBeOfType<RollCreatedEvent>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void GivenInvalidNumberOfPins_WhenInitializing_ThenThrowException(int pinsKnockedOver)
        {
            // Arrange

            // Act
            Action actual = () => TenPinRoll.Create(pinsKnockedOver, _eventRegister);

            // Assert
            actual.Should().ThrowExactly<RollException>();
            _eventRegister.DomainEvents.Should().BeEmpty();
        }
    }
}