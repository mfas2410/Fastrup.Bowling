namespace Fastrup.Bowling.Domain.Tests;

public sealed class TenPinTraditionalScoringTests
{
    private readonly EventRegister _eventRegister;
    private readonly Player[] _players;
    private readonly TenPinTraditionalScore _sut;

    public TenPinTraditionalScoringTests()
    {
        _eventRegister = new EventRegister();
        _players = new[] { Player.Create(new Id(Guid.NewGuid()), new UserName("Player"), _eventRegister) };
        _sut = new TenPinTraditionalScore();
    }

    [Fact]
    public void GivenAFrame_WhenScoring0s_ThenScore0Points()
    {
        // Arrange
        int expected = 0;
        TenPinFrame frame = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true, _eventRegister);
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
        int expected = 15;
        TenPinFrame frame = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true, _eventRegister);
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
        int expected = 30;
        TenPinFrame frame = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true, _eventRegister);
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
        int expected = 0;
        TenPinGame game = TenPinGame.Create(new Id(Guid.NewGuid()), _players, _eventRegister);
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
        int expected = 300;
        TenPinGame game = TenPinGame.Create(new Id(Guid.NewGuid()), _players, _eventRegister);
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
