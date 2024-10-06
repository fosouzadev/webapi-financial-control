using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.Entities;

public class FinancialMovementCategoryTest : BaseTest
{
    [Fact]
    public void Constructor_ValidParameters_CreateObject()
    {
        // Arrange
        Name name = base.Fixture.Create<Name>();
        DateTimeOffset creationDateTime = base.Fixture.Create<DateTimeOffset>();
        Guid id = base.Fixture.Create<Guid>();

        // Act
        FinancialMovementCategory entity = new(name, creationDateTime, id);

        // Assert
        entity.Name.Should().Be(name);
        entity.CreationDateTime.Should().Be(creationDateTime);
        entity.Id.Should().Be(id);
    }
}