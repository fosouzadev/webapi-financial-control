using FluentAssertions;
using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.WebApi;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using System.Text.Json;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi;

public sealed class ApplicationExceptionHandlerTest : BaseTest
{
    private readonly Mock<ILogger<ApplicationExceptionHandler>> _loggerMock;
    private readonly Mock<HttpContext> _httpContextMock;
    private readonly Mock<HttpResponse> _httpResponseMock;

    private readonly ApplicationExceptionHandler _handler;

    public ApplicationExceptionHandlerTest()
    {
        _loggerMock = new Mock<ILogger<ApplicationExceptionHandler>>();
        _httpContextMock = new Mock<HttpContext>();
        
        _httpResponseMock = new Mock<HttpResponse>();
        _httpResponseMock.Setup(a => a.HttpContext).Returns(_httpContextMock.Object);
        _httpResponseMock.Setup(a => a.ContentType).Returns("application/json");
        _httpResponseMock.Setup(a => a.Body).Returns(new MemoryStream());
        _httpResponseMock.SetupProperty(a => a.StatusCode);

        _httpContextMock.Setup(a => a.Response).Returns(_httpResponseMock.Object);

        _handler = new(_loggerMock.Object);
    }

    private async Task ActAndAssertAsync<TException>(
        TException expectedException,
        int expectedHttpStatusCode,
        Expression<Func<TException, bool>> verifyLoggerException)
        where TException : Exception
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
    }

    [Fact]
    public async Task TryHandleAsync_ThrowNotFoundException_ReturnTrue()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        NotFoundException exception = new(expectedId);

        // Act / Assert
        await ActAndAssertAsync(exception, StatusCodes.Status404NotFound, a => a.Id == expectedId);

        _httpResponseMock.Verify(a =>
            a.WriteAsJsonAsync(
                It.Is<ResponseData<Guid>>(b => b.Data == expectedId && b.ErrorMessage == exception.Message),
                It.IsAny<JsonSerializerOptions>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}