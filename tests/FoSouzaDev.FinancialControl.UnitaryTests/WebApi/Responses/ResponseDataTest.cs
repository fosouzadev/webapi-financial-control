using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.WebApi.Responses;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Responses;

public sealed class ResponseDataTest : BaseTest
{
    [Fact]
    public void Constructor_ResponseDataWithData_CreateAnObject()
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
    public void Constructor_ResponseDataWithDataAndErrorMessage_CreateAnObject()
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

    [Fact]
    public void Constructor_ResponseDataWithInvalidData_ThrowException()
    {
        // Arrange
        string expectedErrorMessage = base.Fixture.Create<string>();

        // Act
        Action act = () => new ResponseData<object>(data: null, errorMessage: expectedErrorMessage);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("The parameter cannot be null. (Parameter 'data')");
    }

    [Fact]
    public void Constructor_ResponseDataOnlyWithError_CreateAnObject()
    {
        // Arrange
        string expectedErrorMessage = base.Fixture.Create<string>();

        // Act
        ResponseData responseData = new(errorMessage: expectedErrorMessage);

        // Assert
        responseData.ErrorMessage.Should().Be(expectedErrorMessage);
    }
}