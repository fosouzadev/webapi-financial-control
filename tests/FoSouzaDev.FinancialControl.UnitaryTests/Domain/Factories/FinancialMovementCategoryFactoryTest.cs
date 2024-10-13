using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.Factories;

public sealed class FinancialMovementCategoryFactoryTest : BaseTest
{
    private readonly IFinancialMovementCategoryFactory _factory;

    public FinancialMovementCategoryFactoryTest()
    {
        _factory = new FinancialMovementCategoryFactory();
    }

    [Fact]
    public void CreateEntity_ValidInput_ReturnEntity()
    {
        // Arrange
        string expectedName = base.Fixture.Create<string>();

        // Act
        FinancialMovementCategory entity = _factory.CreateEntity(expectedName);

        // Assert
        entity.Name.Should().Be(new Name(expectedName));
        entity.CreationDateTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void RebuildEntity_ValidInput_ReturnEntity()
    {
        // Arrange
        string expectedName = base.Fixture.Create<string>();
        DateTimeOffset expectedCreationDateTime = base.Fixture.Create<DateTimeOffset>();
        Guid expectedId = base.Fixture.Create<Guid>();

        // Act
        FinancialMovementCategory entity = _factory.RebuildEntity(expectedName, expectedCreationDateTime, expectedId);

        // Assert
        entity.Name.Should().Be(new Name(expectedName));
        entity.CreationDateTime.Should().Be(expectedCreationDateTime);
        entity.Id.Should().Be(expectedId);
    }
}