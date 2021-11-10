namespace Fastrup.Bowling.Domain.Tests;

public sealed class TenPinFrameTests
{
    private readonly EventRegister _eventRegister = new();

    [Fact]
    public void GivenAFrame_WhenKnockingAllPinsInFirstRoll_ThenItsAStrike()
    {
        // Arrange
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false, _eventRegister);
        TenPinRoll roll = TenPinRoll.Create(10, _eventRegister);

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
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true, _eventRegister);
        TenPinRoll roll1 = TenPinRoll.Create(10, _eventRegister);
        TenPinRoll roll2 = TenPinRoll.Create(10, _eventRegister);
        TenPinRoll roll3 = TenPinRoll.Create(10, _eventRegister);

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
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true, _eventRegister);
        TenPinRoll roll1 = TenPinRoll.Create(10, _eventRegister);
        TenPinRoll roll2 = TenPinRoll.Create(3, _eventRegister);
        TenPinRoll roll3 = TenPinRoll.Create(4, _eventRegister);

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
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true, _eventRegister);
        TenPinRoll roll1 = TenPinRoll.Create(7, _eventRegister);
        TenPinRoll roll2 = TenPinRoll.Create(7, _eventRegister);

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
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), true, _eventRegister);
        TenPinRoll roll1 = TenPinRoll.Create(4, _eventRegister);
        TenPinRoll roll2 = TenPinRoll.Create(6, _eventRegister);
        TenPinRoll roll3 = TenPinRoll.Create(7, _eventRegister);

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
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false, _eventRegister);
        sut.AddRoll(TenPinRoll.Create(10, _eventRegister), _eventRegister);
        TenPinRoll roll = TenPinRoll.Create(0, _eventRegister);

        // Act
        Action actual = () => sut.AddRoll(roll, _eventRegister);

        // Assert
        actual.Should().ThrowExactly<FrameException>();
    }

    [Fact]
    public void GivenAFrame_WhenKnockingAllPinsInTwoRolls_ThenItsASpare()
    {
        // Arrange
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false, _eventRegister);
        TenPinRoll roll1 = TenPinRoll.Create(3, _eventRegister);
        TenPinRoll roll2 = TenPinRoll.Create(7, _eventRegister);

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
        TenPinFrame sut = TenPinFrame.Create(new Id(Guid.NewGuid()), new Id(Guid.NewGuid()), false, _eventRegister);
        TenPinRoll roll1 = TenPinRoll.Create(3, _eventRegister);
        TenPinRoll roll2 = TenPinRoll.Create(4, _eventRegister);

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
