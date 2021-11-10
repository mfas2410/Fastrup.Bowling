namespace Fastrup.Bowling.Domain.Tests;

public sealed class TenPinFrameTests
{
    private readonly EventRegister _eventRegister = new();

    [Fact]
    public void GivenAFrame_WhenKnockingAllPinsInFirstRoll_ThenItsAStrike()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false);
        TenPinRoll roll = new(10);

        // Act
        sut.AddRoll(roll);

        // Assert
        sut.IsComplete.Should().BeTrue();
        sut.IsSpare.Should().BeFalse();
        sut.IsStrike.Should().BeTrue();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenLastFrameAsStrike_WhenKnockingAllPinsInFirstRoll_ThenItsAStrikeWithTwoBonusRolls()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true);
        TenPinRoll roll1 = new(10);
        TenPinRoll roll2 = new(10);
        TenPinRoll roll3 = new(10);

        // Act
        sut.AddRoll(roll1);
        sut.AddRoll(roll2);
        sut.AddRoll(roll3);

        // Assert
        sut.IsComplete.Should().BeTrue();
        sut.IsSpare.Should().BeFalse();
        sut.IsStrike.Should().BeTrue();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenLastFrame_WhenKnockingAllPinsInFirstRoll_ThenItsAStrikeWithTwoBonusRolls()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true);
        TenPinRoll roll1 = new(10);
        TenPinRoll roll2 = new(3);
        TenPinRoll roll3 = new(4);

        // Act
        sut.AddRoll(roll1);
        sut.AddRoll(roll2);
        sut.AddRoll(roll3);

        // Assert
        sut.IsComplete.Should().BeTrue();
        sut.IsSpare.Should().BeFalse();
        sut.IsStrike.Should().BeTrue();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenLastFrameWithAllPinsKnockedOverInTwoRolls_WhenKnockingTooManyPinsInSecondRoll_ThrowException()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true);
        TenPinRoll roll1 = new(7);
        TenPinRoll roll2 = new(7);

        // Act
        sut.AddRoll(roll1);
        Action actual = () => sut.AddRoll(roll2);

        // Assert
        actual.Should().ThrowExactly<FrameException>();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenLastFrame_WhenKnockingAllPinsInBothRolls_ThenItsASpareWithOneBonusRoll()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true);
        TenPinRoll roll1 = new(4);
        TenPinRoll roll2 = new(6);
        TenPinRoll roll3 = new(7);

        // Act
        sut.AddRoll(roll1);
        sut.AddRoll(roll2);
        sut.AddRoll(roll3);

        // Assert
        sut.IsComplete.Should().BeTrue();
        sut.IsSpare.Should().BeTrue();
        sut.IsStrike.Should().BeFalse();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenACompletedFrame_WhenAddingRoll_ThenThrowException()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false);
        TenPinRoll roll1 = new(10);
        sut.AddRoll(roll1);
        TenPinRoll roll2 = new(0);

        // Act
        Action actual = () => sut.AddRoll(roll2);

        // Assert
        actual.Should().ThrowExactly<FrameException>();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenAFrame_WhenKnockingAllPinsInTwoRolls_ThenItsASpare()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false);
        TenPinRoll roll1 = new(3);
        TenPinRoll roll2 = new(7);

        // Act
        sut.AddRoll(roll1);
        sut.AddRoll(roll2);

        // Assert
        sut.IsComplete.Should().BeTrue();
        sut.IsSpare.Should().BeTrue();
        sut.IsStrike.Should().BeFalse();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenAFrame_WhenOnlyKnockingSomePinsInTwoRolls_ThenMarkFrameAsComplete()
    {
        // Arrange
        TenPinFrame sut = new(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false);
        TenPinRoll roll1 = new(3);
        TenPinRoll roll2 = new(4);

        // Act
        sut.AddRoll(roll1);
        sut.AddRoll(roll2);

        // Assert
        sut.IsComplete.Should().BeTrue();
        sut.IsSpare.Should().BeFalse();
        sut.IsStrike.Should().BeFalse();
        _eventRegister.DomainEvents.Should().BeEmpty();
    }
}
