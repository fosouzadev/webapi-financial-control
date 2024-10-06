using AutoFixture;
using FluentAssertions;
using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.ValueObjects;

public class NameTest : BaseTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_InvalidName_ThrowException(string name)
    {
        // Act
        Action act = () => _ = new Name(name);

        // Assert
        act.Should().ThrowExactly<ValidateException>()
            .WithMessage($"{nameof(Name)} cannot be null or empty.");
    }

    [Fact]
    public void Constructor_ValidName_CreateObject()
    {
        // Arrange
        string name = base.Fixture.Create<string>();

        // Act
        Name valueObject = new(name);

        // Assert
        valueObject.Value.Should().Be(name);
    }

    [Fact]
    public void Equals_SameValueRepresentsTheSameObject_ReturnsTrue()
    {
        // Arrange
        string name = base.Fixture.Create<string>();
        Name valueObject1 = new(name);
        Name valueObject2 = new(name);

        // Act
        bool equals = valueObject1.Equals(valueObject2) && valueObject1 == valueObject2;

        // Assert
        equals.Should().BeTrue();
    }
}