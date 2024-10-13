using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.Factories;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using Moq;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.Factories;

public sealed class FinancialMovementFactoryTest : BaseTest
{
    private readonly IFinancialMovementFactory _factory;

    private readonly Mock<IBankAccountRepository> _bankAccountRepositoryMock;
    private readonly Mock<IFinancialMovementCategoryRepository> _financialMovementCategoryRepositoryMock;

    public FinancialMovementFactoryTest()
    {
        _bankAccountRepositoryMock = new();
        _financialMovementCategoryRepositoryMock = new();

        _factory = new FinancialMovementFactory(
            _bankAccountRepositoryMock.Object,
            _financialMovementCategoryRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateEntityAsync_ValidInput_ReturnEntity()
    {
        // Arrange
        string expectedName = base.Fixture.Create<string>();
        decimal expectedAmount = base.Fixture.Create<decimal>();
        FinancialMovementType expectedType = base.Fixture.Create<FinancialMovementType>();
        Guid expectedCategoryId = base.Fixture.Create<Guid>();
        Guid expectedBankAccountId = base.Fixture.Create<Guid>();

        FinancialMovementCategory expectedCategory = base.Fixture.Create<FinancialMovementCategory>();
        _financialMovementCategoryRepositoryMock.Setup(a => a.GetByIdOrThrowAsync(expectedCategoryId))
            .ReturnsAsync(expectedCategory);

        BankAccount expectedBankAccount = base.Fixture.Create<BankAccount>();
        _bankAccountRepositoryMock.Setup(a => a.GetByIdOrThrowAsync(expectedBankAccountId))
            .ReturnsAsync(expectedBankAccount);

        // Act
        FinancialMovement entity = await _factory.CreateEntityAsync(
            expectedName,
            expectedAmount,
            (byte)expectedType,
            expectedCategoryId,
            expectedBankAccountId);

        // Assert
        entity.Name.Should().Be(new Name(expectedName));
        entity.Amount.Should().Be(new Amount(expectedAmount));
        entity.Type.Should().Be(expectedType);
        entity.Category.Should().Be(expectedCategory);
        entity.BankAccount.Should().Be(expectedBankAccount);
        entity.CreationDateTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateEntityAsync_InvalidFinancialMovementType_ThrowException()
    {
        // Arrange
        byte invalidType = 0;

        // Act
        Func<Task> act = () => _factory.CreateEntityAsync(
            base.Fixture.Create<string>(),
            base.Fixture.Create<decimal>(),
            invalidType,
            base.Fixture.Create<Guid>(),
            base.Fixture.Create<Guid>());

        // Assert
        (await act.Should().ThrowAsync<ArgumentException>())
            .WithMessage($"Invalid value for enum {nameof(FinancialMovementType)}: {invalidType}");
    }
}