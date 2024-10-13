using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.Factories;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.Factories;

public sealed class BankAccountFactoryTest : BaseTest
{
    private readonly IBankAccountFactory _factory;

    public BankAccountFactoryTest()
    {
        _factory = new BankAccountFactory();

        base.Fixture.Customize<BankAccountCreateDto>(a => a.With(b => b.Type, (byte)base.Fixture.Create<BankAccountType>()));
        base.Fixture.Customize<BankAccountRebuildDto>(a => a.With(b => b.Type, (byte)base.Fixture.Create<BankAccountType>()));
    }

    [Fact]
    public async Task CreateEntityAsync_ValidInput_ReturnEntity()
    {
        // Arrange
        BankAccountCreateDto createDto = base.Fixture.Create<BankAccountCreateDto>();

        // Act
        BankAccount entity = await _factory.CreateEntityAsync(createDto);

        // Assert
        entity.Name.Should().Be(new Name(createDto.Name));
        entity.Description.Should().Be(createDto.Description);
        entity.IsActive.Should().BeTrue();
        entity.Type.Should().Be((BankAccountType)createDto.Type);
        entity.Balance.Should().Be(0);
        entity.CreationDateTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateEntityAsync_InvalidBankAccountType_ThrowException()
    {
        // Arrange
        BankAccountCreateDto createDto = base.Fixture.Build<BankAccountCreateDto>()
            .With(a => a.Type, 0)
            .Create();

        // Act
        Func<Task> act = () => _factory.CreateEntityAsync(createDto);

        // Assert
        (await act.Should().ThrowAsync<ArgumentException>())
            .WithMessage($"Invalid value for enum {nameof(BankAccountType)}: {createDto.Type}");
    }

    [Fact]
    public async Task RebuildEntityAsync_ValidInput_ReturnEntity()
    {
        // Arrange
        BankAccountRebuildDto rebuildDto = base.Fixture.Create<BankAccountRebuildDto>();

        // Act
        BankAccount entity = await _factory.RebuildEntityAsync(rebuildDto);

        // Assert
        entity.Name.Should().Be(new Name(rebuildDto.Name));
        entity.Description.Should().Be(rebuildDto.Description);
        entity.IsActive.Should().Be(rebuildDto.IsActive);
        entity.Type.Should().Be((BankAccountType)rebuildDto.Type);
        entity.Balance.Should().Be(rebuildDto.Balance);
        entity.CreationDateTime.Should().Be(rebuildDto.CreationDateTime);
        entity.Id.Should().Be(rebuildDto.Id);
    }

    [Fact]
    public async Task RebuildEntityAsync_InvalidBankAccountType_ThrowException()
    {
        // Arrange
        BankAccountRebuildDto rebuildDto = base.Fixture.Build<BankAccountRebuildDto>()
            .With(a => a.Type, 0)
            .Create();

        // Act
        Func<Task> act = () => _factory.RebuildEntityAsync(rebuildDto);

        // Assert
        (await act.Should().ThrowAsync<ArgumentException>())
            .WithMessage($"Invalid value for enum {nameof(BankAccountType)}: {rebuildDto.Type}");
    }
}