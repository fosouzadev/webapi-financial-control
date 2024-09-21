using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.WebApi.Responses;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Responses;

public sealed class ResponseDataTest : BaseTest
{
    [Fact]
    public void Constructor_WithData_CreateAnObject()
    {
        // Arrange
        string expectedData = base.Fixture.Create<string>();

        // Act
        ResponseData<string> responseData = new(data: expectedData);

        // Assert
        responseData.Data.Should().Be(expectedData);
        responseData.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithErrorMessage_CreateAnObject()
    {
        // Arrange
        string expectedErrorMessage = base.Fixture.Create<string>();

        // Act
        ResponseData<object> responseData = new(data: null, errorMessage: expectedErrorMessage);

        // Assert
        responseData.Data.Should().BeNull();
        responseData.ErrorMessage.Should().Be(expectedErrorMessage);
    }

    [Fact]
    public void Constructor_WithDataAndErrorMessage_CreateAnObject()
    {
        // Arrange
        string expectedData = base.Fixture.Create<string>();
        string expectedErrorMessage = base.Fixture.Create<string>();

        // Act
        ResponseData<string> responseData = new(expectedData, expectedErrorMessage);

        // Assert
        responseData.Data.Should().Be(expectedData);
        responseData.ErrorMessage.Should().Be(expectedErrorMessage);
    }
}