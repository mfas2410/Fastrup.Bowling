namespace Fastrup.Bowling.Domain.Tests;

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
    public void GivenNewSinglePlayerGame_WhenCreating_ThenGameHasIdAndEventsAreRegistered()
    {
        // Arrange
        Id expected = _sut.Id;

        // Act
        Guid actual = _sut.Id;

        // Assert
        actual.Should().NotBeEmpty().And.Be(expected);
        _eventRegister.DomainEvents.Should().ContainSingle(x => x is PlayerCreatedEvent);
        _eventRegister.DomainEvents.Should().ContainSingle(x => x is GameCreatedEvent);
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
            _sut.AddRoll(new TenPinRoll(0));
            numberOfRolls++;
        }

        // Assert
        _sut.IsComplete.Should().BeTrue();
        _eventRegister.DomainEvents.Where(x => x is RollCreatedEvent).Should().HaveCount(expectedNumberOfRolls);
        _eventRegister.DomainEvents.Where(x => x is FrameCreatedEvent).Should().HaveCount(expectedNumberOfRolls / 2);
        _eventRegister.DomainEvents.Where(x => x is FrameCompletedEvent).Should().HaveCount(expectedNumberOfRolls / 2);
        _eventRegister.DomainEvents.Should().ContainSingle(x => x is GameCompletedEvent);
        numberOfRolls.Should().Be(expectedNumberOfRolls);
    }

    [Fact]
    public void GivenATwoPlayerGame_WhenCompletingAllFrames_ThenCompleteGame()
    {
        // Arrange
        int expectedNumberOfRolls = 40;
        int numberOfRolls = 0;
        TenPinGame sut = TenPinGame.Create(new Id(Guid.NewGuid()), CreatePlayers(2, _eventRegister), _eventRegister);

        // Act
        while (!sut.IsComplete)
        {
            TenPinRoll roll = new(0);
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
            TenPinRoll roll = new(0);
            _sut.AddRoll(roll);
        }

        // Act
        Action actual = () =>
        {
            TenPinRoll roll = new(0);
            _sut.AddRoll(roll);
        };

        // Assert
        actual.Should().ThrowExactly<GameException>();
    }

    private static IEnumerable<Player> CreatePlayers(int numberOfPlayers, IEventRegister eventRegister)
    {
        List<Player> players = [];
        Enumerable.Range(1, numberOfPlayers).ToList().ForEach(number => players.Add(Player.Create(new Id(Guid.NewGuid()), new UserName($"Player {number}"), eventRegister)));
        return players;
    }
}
