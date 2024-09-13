//using AutoFixture;
//using FluentAssertions;
//using FoSouzaDev.Common.Domain.Exceptions;
//using FoSouzaDev.FinancialControl.Domain.Entities;
//using FoSouzaDev.FinancialControl.Domain.ValueObjects;

//namespace FoSouzaDev.FinancialControl.UnitaryTests.Domain;

//public class FinancialMovementCategoryTest : BaseTest
//{
//    [Theory]
//    [InlineData(null)]
//    [InlineData("")]
//    [InlineData(" ")]
//    public void Constructor_InvalidName_ThrowException(string name)
//    {
//        // Act
//        Action act = () => _ = new FinancialMovementCategory(new Name(name));

//        // Assert
//        act.Should().ThrowExactly<ValidateException>()
//            .WithMessage($"{nameof(FinancialMovementCategory.Name)} cannot be null or empty.");
//    }

//    [Fact]
//    public void Constructor_ObjectReconstruction_CreateObject()
//    {
//        // Arrange
//        Name name = base.Fixture.Create<Name>();
//        Guid id = Guid.NewGuid();

//        // Act
//        FinancialMovementCategory category = new(name, id);

//        // Assert
//        category.Name.Should().Be(name);
//        category.Id.Should().Be(id);
//    }

//    [Fact]
//    public void Constructor_NewObject_CreateObject()
//    {
//        // Arrange
//        Name name = base.Fixture.Create<Name>();

//        // Act
//        FinancialMovementCategory category = new(name);

//        // Assert
//        category.Name.Should().Be(name);
//        category.Id.Should().NotBe((Guid)default);
//    }
//}