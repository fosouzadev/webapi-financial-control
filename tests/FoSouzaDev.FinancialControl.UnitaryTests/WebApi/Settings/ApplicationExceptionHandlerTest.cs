using AutoFixture;
using FluentAssertions;
using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using FoSouzaDev.FinancialControl.WebApi.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Settings;

public sealed class ApplicationExceptionHandlerTest : BaseTest
{
    private readonly Mock<ILogger<ApplicationExceptionHandler>> _loggerMock;
    private readonly Mock<HttpContext> _httpContextMock;
    private readonly Mock<HttpResponse> _httpResponseMock;
    private readonly Mock<IHttpResponseWriter> _httpResponseWriterMock;

    private readonly ApplicationExceptionHandler _handler;

    public ApplicationExceptionHandlerTest()
    {
        _loggerMock = new Mock<ILogger<ApplicationExceptionHandler>>();
        _httpResponseWriterMock = new Mock<IHttpResponseWriter>();
        _httpContextMock = new Mock<HttpContext>();

        _httpResponseMock = new Mock<HttpResponse>();
        _httpResponseMock.Setup(a => a.HttpContext).Returns(_httpContextMock.Object);
        _httpResponseMock.Setup(a => a.ContentType).Returns("application/json");
        _httpResponseMock.Setup(a => a.Body).Returns(new MemoryStream());
        _httpResponseMock.SetupProperty(a => a.StatusCode);

        _httpContextMock.Setup(a => a.Response).Returns(_httpResponseMock.Object);

        _handler = new(_loggerMock.Object, _httpResponseWriterMock.Object);
    }

    private async Task ActAndAssertAsync<TException, TResponseData>(
        TException expectedException,
        int expectedHttpStatusCode,
        Expression<Func<TException, bool>> verifyLoggerException,
        Expression<Func<TResponseData, bool>> verifyWriteResponse)
        where TException : Exception
        where TResponseData : ResponseBase
    {
        // Act
        bool act = await _handler.TryHandleAsync(_httpContextMock.Object, expectedException, CancellationToken.None);

        // Assert
        act.Should().BeTrue();

        _httpResponseMock.Object.StatusCode.Should().Be(expectedHttpStatusCode);

        _loggerMock.Verify(a =>
            a.Log(
                It.Is<LogLevel>(b => b == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Exception message: {expectedException.Message}")),
                It.Is(verifyLoggerException),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);

        _httpResponseWriterMock.Verify(a =>
            a.WriteAsJsonAsync(
                _httpResponseMock.Object,
                It.Is(verifyWriteResponse),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task TryHandleAsync_ThrowValidateException_ReturnTrue()
    {
        // Arrange
        ValidateException exception = base.Fixture.Create<ValidateException>();

        // Act / Assert
        await ActAndAssertAsync<ValidateException, ResponseData>(
            exception,
            StatusCodes.Status400BadRequest,
            ex => ex.Message == exception.Message,
            responseData => responseData.ErrorMessage == exception.Message);
    }

    [Fact]
    public async Task TryHandleAsync_ThrowJsonPatchException_ReturnTrue()
    {
        // Arrange
        JsonPatchException exception = base.Fixture.Create<JsonPatchException>();

        // Act / Assert
        await ActAndAssertAsync<JsonPatchException, ResponseData>(
            exception,
            StatusCodes.Status400BadRequest,
            ex => ex.Message == exception.Message,
            responseData => responseData.ErrorMessage == exception.Message);
    }

    [Fact]
    public async Task TryHandleAsync_ThrowNotFoundException_ReturnTrue()
    {
        // Arrange
        NotFoundException exception = base.Fixture.Create<NotFoundException>();

        // Act / Assert
        await ActAndAssertAsync<NotFoundException, ResponseData<Guid>>(
            exception,
            StatusCodes.Status404NotFound,
            ex => ex.Id == exception.Id && ex.Message == exception.Message,
            responseData => responseData.Data == exception.Id && responseData.ErrorMessage == exception.Message);
    }

    [Fact]
    public async Task TryHandleAsync_ThrowConflictException_ReturnTrue()
    {
        // Arrange
        ConflictException exception = base.Fixture.Create<ConflictException>();

        // Act / Assert
        await ActAndAssertAsync<ConflictException, ResponseData<Guid>>(
            exception,
            StatusCodes.Status409Conflict,
            ex => ex.Id == exception.Id && ex.Message == exception.Message,
            responseData => responseData.Data == exception.Id && responseData.ErrorMessage == exception.Message);
    }

    [Fact]
    public async Task TryHandleAsync_ThrowException_ReturnTrue()
    {
        // Arrange
        Exception exception = base.Fixture.Create<Exception>();

        // Act / Assert
        await ActAndAssertAsync<Exception, ResponseData>(
            exception,
            StatusCodes.Status500InternalServerError,
            ex => ex.Message == exception.Message,
            responseData => responseData.ErrorMessage == "Internal server error.");
    }
}