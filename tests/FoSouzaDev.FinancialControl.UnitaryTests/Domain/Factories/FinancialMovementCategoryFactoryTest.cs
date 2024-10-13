using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
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
    public async Task CreateEntityAsync_ValidInput_ReturnEntity()
    {
        // Arrange
        FinancialMovementCategoryCreateDto createDto = base.Fixture.Create<FinancialMovementCategoryCreateDto>();

        // Act
        FinancialMovementCategory entity = await _factory.CreateEntityAsync(createDto);

        // Assert
        entity.Name.Should().Be(new Name(createDto.Name));
        entity.CreationDateTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task RebuildEntityAsync_ValidInput_ReturnEntity()
    {
        // Arrange
        FinancialMovementCategoryRebuildDto rebuildDto = base.Fixture.Create<FinancialMovementCategoryRebuildDto>();

        // Act
        FinancialMovementCategory entity = await _factory.RebuildEntityAsync(rebuildDto);

        // Assert
        entity.Name.Should().Be(new Name(rebuildDto.Name));
        entity.CreationDateTime.Should().Be(rebuildDto.CreationDateTime);
        entity.Id.Should().Be(rebuildDto.Id);
    }
}