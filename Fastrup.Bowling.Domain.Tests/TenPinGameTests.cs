using System;
using Fastrup.Bowling.Domain.Events;
using Fastrup.Bowling.Domain.Exceptions;
using Fastrup.Bowling.Domain.Model.Game;
using FluentAssertions;
using Xunit;

namespace Fastrup.Bowling.Tests
{
    public sealed class TenPinGameTests
    {
        private readonly EventRegister _eventRegister = new();
        private readonly TenPinGame _sut;

        public TenPinGameTests() => _sut = TenPinGame.Create(_eventRegister);

        [Fact]
        public void GivenGame_WhenCreating_ThenGameCreatedEventIsRegistered()
        {
            // Arrange

            // Act

            // Assert
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is GameCreatedEvent);
        }

        [Fact]
        public void GivenGame_WhenCreating_ThenGameHasAnId()
        {
            // Arrange
            Guid expected = _sut.Id;

            // Act
            Guid actual = _sut.Id;

            // Assert
            actual.Should().NotBeEmpty().And.Be(expected);
        }

        [Fact]
        public void GivenAGame_WhenCompletingAllFrames_ThenCompleteGame()
        {
            // Arrange

            // Act
            while (!_sut.IsComplete)
            {
                _sut.AddRoll(TenPinRoll.Create(0, _eventRegister));
            }

            // Assert
            _sut.IsComplete.Should().BeTrue();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is GameCompletedEvent);
        }

        [Fact]
        public void GivenACompletedGame_WhenAddingRoll_ThenThrowException()
        {
            // Arrange
            while (!_sut.IsComplete)
            {
                _sut.AddRoll(TenPinRoll.Create(0, _eventRegister));
            }

            // Act
            Action actual = () => _sut.AddRoll(TenPinRoll.Create(0, _eventRegister));

            // Assert
            actual.Should().ThrowExactly<GameException>();
        }
    }
}