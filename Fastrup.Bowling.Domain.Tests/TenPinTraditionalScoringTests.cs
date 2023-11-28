namespace Fastrup.Bowling.Domain.Tests;

public sealed class TenPinTraditionalScoringTests
{
    private readonly EventRegister _eventRegister;
    private readonly TenPinGame _game;
    private readonly TenPinTraditionalScore _sut;

    public TenPinTraditionalScoringTests()
    {
        _eventRegister = new EventRegister();
        Player[] players = { Player.Create(new Id(Guid.NewGuid()), new UserName("Player"), _eventRegister) };
        _game = TenPinGame.Create(new Id(Guid.NewGuid()), players, _eventRegister);
        _sut = new TenPinTraditionalScore();
    }

    [Fact]
    public void GivenAFrame_WhenScoring0s_ThenScore0Points()
    {
        // Arrange
        int expected = 0;
        while (!_eventRegister.DomainEvents.Any(x => x is FrameCompletedEvent))
        {
            TenPinRoll roll = new(0);
            _game.AddRoll(roll);
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
        int expected = 25;
        while (_eventRegister.DomainEvents.Count(x => x is FrameCompletedEvent) < 2)
        {
            TenPinRoll roll = new(5);
            _game.AddRoll(roll);
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
        int expected = 30;
        while (_eventRegister.DomainEvents.Count(x => x is FrameCompletedEvent) < 2)
        {
            TenPinRoll roll = new(10);
            _game.AddRoll(roll);
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
        int expected = 0;
        while (!_game.IsComplete)
        {
            TenPinRoll roll = new(0);
            _game.AddRoll(roll);
        }

        AddFrameCompletedEvents();

        // Act
        int actual = _sut.Score;

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenAGame_WhenOnlyScoringStrikes_ThenScore300Points()
    {
        // Arrange
        int expected = 300;
        while (!_game.IsComplete)
        {
            TenPinRoll roll = new(10);
            _game.AddRoll(roll);
        }

        AddFrameCompletedEvents();

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
