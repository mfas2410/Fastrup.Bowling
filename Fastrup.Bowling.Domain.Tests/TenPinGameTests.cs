using Fastrup.Bowling.Domain.Abstractions;
using Fastrup.Bowling.Domain.Events;
using Fastrup.Bowling.Domain.Exceptions;
using Fastrup.Bowling.Domain.Model;
using Fastrup.Bowling.Domain.Model.Game;
using Fastrup.Bowling.Domain.Model.Player;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fastrup.Bowling.Domain.Tests
{
    public sealed class TenPinGameTests
    {
        private readonly EventRegister _eventRegister = new();
        private readonly TenPinGame _sut;

        public TenPinGameTests()
        {
            IEnumerable<Player> players = CreatePlayers(1, _eventRegister);
            _sut = TenPinGame.Create(new Id(Guid.NewGuid()), players, _eventRegister);
        }

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
            Id expected = _sut.Id;

            // Act
            Id actual = _sut.Id;

            // Assert
            actual.Value.Should().NotBeEmpty().And.Be(expected.Value);
        }

        [Fact]
        public void GivenASinglePlayerGame_WhenCompletingAllFrames_ThenCompleteGame()
        {
            // Arrange
            int expectedNumberOfRolls = 20;
            int numberOfRolls = 0;

            // Act
            while (!_sut.IsComplete)
            {
                _sut.AddRoll(TenPinRoll.Create(0, _eventRegister));
                numberOfRolls++;
            }

            // Assert
            _sut.IsComplete.Should().BeTrue();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is GameCompletedEvent);
            numberOfRolls.Should().Be(expectedNumberOfRolls);
        }

        [Fact]
        public void GivenATwoPlayerGame_WhenCompletingAllFrames_ThenCompleteGame()
        {
            // Arrange
            int expectedNumberOfRolls = 40;
            int numberOfRolls = 0;
            var sut = TenPinGame.Create(new Id(Guid.NewGuid()), CreatePlayers(2, _eventRegister), _eventRegister);

            // Act
            while (!sut.IsComplete)
            {
                var roll = TenPinRoll.Create(0, _eventRegister);
                sut.AddRoll(roll);
                numberOfRolls++;
            }

            // Assert
            sut.IsComplete.Should().BeTrue();
            _eventRegister.DomainEvents.Should().ContainSingle(x => x is GameCompletedEvent);
            numberOfRolls.Should().Be(expectedNumberOfRolls);
        }

        [Fact]
        public void GivenACompletedGame_WhenAddingRoll_ThenThrowException()
        {
            // Arrange
            while (!_sut.IsComplete)
            {
                var roll = TenPinRoll.Create(0, _eventRegister);
                _sut.AddRoll(roll);
            }

            // Act
            Action actual = () =>
            {
                var roll = TenPinRoll.Create(0, _eventRegister);
                _sut.AddRoll(roll);
            };

            // Assert
            actual.Should().ThrowExactly<GameException>();
        }

        private static IEnumerable<Player> CreatePlayers(int numberOfPlayers, IEventRegister eventRegister)
        {
            List<Player> players = new();
            Enumerable.Range(1, numberOfPlayers).ToList().ForEach(number => players.Add(Player.Create(new Id(Guid.NewGuid()), new UserName($"Player {number}"), eventRegister)));
            return players;
        }
    }
}