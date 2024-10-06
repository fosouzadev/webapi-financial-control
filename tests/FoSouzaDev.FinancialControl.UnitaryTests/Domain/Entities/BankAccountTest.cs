using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain.Entities;

public class BankAccountTest : BaseTest
{
    [Fact]
    public void Constructor_ValidParameters_CreateObject()
    {
        // Arrange
        Name name = base.Fixture.Create<Name>();
        string description = base.Fixture.Create<string>();
        bool isActive = base.Fixture.Create<bool>();
        BankAccountType bankAccountType = base.Fixture.Create<BankAccountType>();
        decimal balance = base.Fixture.Create<decimal>();
        DateTimeOffset creationDateTime = base.Fixture.Create<DateTimeOffset>();
        Guid id = base.Fixture.Create<Guid>();

        // Act
        BankAccount entity = new(
            name,
            description,
            isActive,
            bankAccountType,
            balance,
            creationDateTime,
            id);

        // Assert
        entity.Name.Should().Be(name);
        entity.Description.Should().Be(description);
        entity.IsActive.Should().Be(isActive);
        entity.Type.Should().Be(bankAccountType);
        entity.Balance.Should().Be(balance);
        entity.CreationDateTime.Should().Be(creationDateTime);
        entity.Id.Should().Be(id);
    }
}