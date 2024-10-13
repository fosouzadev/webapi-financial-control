using AutoFixture;
using FluentAssertions;
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
    }

    [Fact]
    public void CreateEntity_ValidInput_ReturnEntity()
    {
        // Arrange
        string expectedName = base.Fixture.Create<string>();
        string expectedDescription = base.Fixture.Create<string>();
        BankAccountType expectedType = base.Fixture.Create<BankAccountType>();

        // Act
        BankAccount entity = _factory.CreateEntity(expectedName, expectedDescription, (byte)expectedType);

        // Assert
        entity.Name.Should().Be(new Name(expectedName));
        entity.Description.Should().Be(expectedDescription);
        entity.IsActive.Should().BeTrue();
        entity.Type.Should().Be(expectedType);
        entity.Balance.Should().Be(0);
        entity.CreationDateTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void CreateEntity_InvalidBankAccountType_ThrowException()
    {
        // Arrange
        byte invalidType = 0;

        // Act
        Action act = () => _factory.CreateEntity(base.Fixture.Create<string>(), base.Fixture.Create<string>(), invalidType);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage($"Invalid value for enum {nameof(BankAccountType)}: {invalidType}");
    }

    [Fact]
    public void RebuildEntity_ValidInput_ReturnEntity()
    {
        // Arrange
        string expectedName = base.Fixture.Create<string>();
        string expectedDescription = base.Fixture.Create<string>();
        bool expectedIsActive = base.Fixture.Create<bool>();
        BankAccountType expectedType = base.Fixture.Create<BankAccountType>();
        decimal expectedBalance = base.Fixture.Create<decimal>();
        DateTimeOffset expectedCreationDateTime = base.Fixture.Create<DateTimeOffset>();
        Guid expectedId = base.Fixture.Create<Guid>();

        // Act
        BankAccount entity = _factory.RebuildEntity(
            expectedName,
            expectedDescription,
            expectedIsActive,
            type: (byte)expectedType,
            expectedBalance,
            expectedCreationDateTime,
            expectedId);

        // Assert
        entity.Name.Should().Be(new Name(expectedName));
        entity.Description.Should().Be(expectedDescription);
        entity.IsActive.Should().Be(expectedIsActive);
        entity.Type.Should().Be(expectedType);
        entity.Balance.Should().Be(expectedBalance);
        entity.CreationDateTime.Should().Be(expectedCreationDateTime);
        entity.Id.Should().Be(expectedId);
    }

    [Fact]
    public void RebuildEntity_InvalidBankAccountType_ThrowException()
    {
        // Arrange
        byte invalidType = 0;

        // Act
        Action act = () => _factory.RebuildEntity(
            base.Fixture.Create<string>(),
            base.Fixture.Create<string>(),
            base.Fixture.Create<bool>(),
            invalidType,
            base.Fixture.Create<decimal>(),
            base.Fixture.Create<DateTimeOffset>(),
            base.Fixture.Create<Guid>()
            );

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage($"Invalid value for enum {nameof(BankAccountType)}: {invalidType}");
    }
}