namespace Fastrup.Bowling.Domain.Tests;

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
    public void GivenValidNumberOfPins_WhenInitializing_ThenReturnNumberOfPins(int pinsKnockedOver)
    {
        // Arrange

        // Act
        TenPinRoll actual = new(pinsKnockedOver);

        // Assert
        actual.PinsKnockedOver.Should().Be(pinsKnockedOver);
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void GivenInvalidNumberOfPins_WhenInitializing_ThenThrowException(int pinsKnockedOver)
    {
        // Arrange

        // Act
        Action actual = () => new TenPinRoll(pinsKnockedOver);

        // Assert
        actual.Should().ThrowExactly<RollException>();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }
}
