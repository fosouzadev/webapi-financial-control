using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.Entities;

public class FinancialMovementTest : BaseTest
{
    [Fact]
    public void Constructor_ValidParameters_CreateObject()
    {
        // Arrange
        Name name = base.Fixture.Create<Name>();
        Amount amount = base.Fixture.Create<Amount>();
        FinancialMovementType financialMovementType = base.Fixture.Create<FinancialMovementType>();
        FinancialMovementCategory category = base.Fixture.Create<FinancialMovementCategory>();
        BankAccount bankAccount = base.Fixture.Create<BankAccount>();
        DateTimeOffset creationDateTime = base.Fixture.Create<DateTimeOffset>();
        Guid id = base.Fixture.Create<Guid>();

        // Act
        FinancialMovement entity = new(
            name,
            amount,
            financialMovementType,
            category,
            bankAccount,
            creationDateTime,
            id);

        // Assert
        entity.Name.Should().Be(name);
        entity.Amount.Should().Be(amount);
        entity.Type.Should().Be(financialMovementType);
        entity.Category.Should().Be(category);
        entity.BankAccount.Should().Be(bankAccount);
        entity.CreationDateTime.Should().Be(creationDateTime);
        entity.Id.Should().Be(id);
    }
}