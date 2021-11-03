using System.Collections.Generic;
using System.Linq;
using Fastrup.Bowling.Domain.Events;
using Fastrup.Bowling.Domain.Model.Game;
using Fastrup.Bowling.Domain.Model.Score;
using FluentAssertions;
using Xunit;

namespace Fastrup.Bowling.Tests
{
    public sealed class TenPinTraditionalScoringTests
    {
        private readonly EventRegister _eventRegister;
        private readonly TenPinTraditionalScore _sut;

        public TenPinTraditionalScoringTests()
        {
            _eventRegister = new EventRegister();
            _sut = new TenPinTraditionalScore();
        }

        [Fact]
        public void GivenAFrame_WhenScoring0s_ThenScore0Points()
        {
            // Arrange
            var expected = 0;
            var frame = TenPinFrame.Create(true, _eventRegister);
            while (!frame.IsComplete)
            {
                frame.AddRoll(TenPinRoll.Create(0, _eventRegister), _eventRegister);
            }

            AddFrameCompletedEvents();

            // Act
            int actual = _sut.Score;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAFrame_WhenScoringASpare_ThenScore10PlusBonusRollPoints()
        {
            // Arrange
            var expected = 15;
            var frame = TenPinFrame.Create(true, _eventRegister);
            while (!frame.IsComplete)
            {
                frame.AddRoll(TenPinRoll.Create(5, _eventRegister), _eventRegister);
            }

            AddFrameCompletedEvents();

            // Act
            int actual = _sut.Score;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAFrame_WhenScoringAStrike_ThenScore10PlusBonusRollPoints()
        {
            // Arrange
            var expected = 30;
            var frame = TenPinFrame.Create(true, _eventRegister);
            while (!frame.IsComplete)
            {
                frame.AddRoll(TenPinRoll.Create(10, _eventRegister), _eventRegister);
            }

            AddFrameCompletedEvents();

            // Act
            int actual = _sut.Score;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAGame_WhenOnlyScoring0s_ThenScore0Points()
        {
            // Arrange
            var expected = 0;
            var game = TenPinGame.Create(_eventRegister);
            while (!game.IsComplete)
            {
                game.AddRoll(TenPinRoll.Create(0, _eventRegister));
            }

            IEnumerable<FrameCompletedEvent> events = _eventRegister.DomainEvents.FindAll(x => x is FrameCompletedEvent).Cast<FrameCompletedEvent>();
            foreach (FrameCompletedEvent frameCompletedEvent in events)
            {
                _sut.AddEvent(frameCompletedEvent);
            }

            // Act
            int actual = _sut.Score;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAGame_WhenOnlyScoringStrikes_ThenScore300Points()
        {
            // Arrange
            var expected = 300;
            var game = TenPinGame.Create(_eventRegister);
            while (!game.IsComplete)
            {
                game.AddRoll(TenPinRoll.Create(10, _eventRegister));
            }

            IEnumerable<FrameCompletedEvent> events = _eventRegister.DomainEvents.FindAll(x => x is FrameCompletedEvent).Cast<FrameCompletedEvent>();
            foreach (FrameCompletedEvent frameCompletedEvent in events)
            {
                _sut.AddEvent(frameCompletedEvent);
            }

            // Act
            int actual = _sut.Score;

            // Assert
            actual.Should().Be(expected);
        }

        private void AddFrameCompletedEvents()
        {
            IEnumerable<FrameCompletedEvent> events = _eventRegister.DomainEvents.FindAll(x => x is FrameCompletedEvent).Cast<FrameCompletedEvent>();
            foreach (FrameCompletedEvent frameCompletedEvent in events)
            {
                _sut.AddEvent(frameCompletedEvent);
            }
        }
    }
}