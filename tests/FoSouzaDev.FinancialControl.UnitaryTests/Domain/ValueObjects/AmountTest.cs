using AutoFixture;
using FluentAssertions;
using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.ValueObjects;

public class AmountTest : BaseTest
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(0)]
    public void Constructor_InvalidAmount_ThrowException(decimal amount)
    {
        // Act
        Action act = () => _ = new Amount(amount);

        // Assert
        act.Should().ThrowExactly<ValidateException>()
            .WithMessage($"{nameof(Amount)} cannot be less than or equal to zero.");
    }

    [Fact]
    public void Constructor_ValidAmount_CreateObject()
    {
        // Arrange
        decimal amount = base.Fixture.Create<decimal>();

        // Act
        Amount valueObject = new(amount);

        // Assert
        valueObject.Value.Should().Be(amount);
    }

    [Fact]
    public void Equals_SameValueRepresentsTheSameObject_ReturnsTrue()
    {
        // Arrange
        decimal amount = base.Fixture.Create<decimal>();
        Amount valueObject1 = new(amount);
        Amount valueObject2 = new(amount);

        // Act
        bool equals = valueObject1.Equals(valueObject2) && valueObject1 == valueObject2;

        // Assert
        equals.Should().BeTrue();
    }
}