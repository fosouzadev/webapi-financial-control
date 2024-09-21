using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Newtonsoft.Json;

namespace FoSouzaDev.FinancialControl.WebApi.Settings;

public sealed class ApplicationExceptionHandler(
    ILogger<ApplicationExceptionHandler> logger,
    IHttpResponseWriter httpResponseWriter)
    : IExceptionHandler
{
    private async Task WriteResponseAsync<T>(
        HttpResponse httpResponse,
        int statusCode,
        ResponseData<T> responseData,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpResponse.StatusCode = statusCode;

        logger.LogError(exception, message: "Response: {Response} - Exception message: {ExceptionMessage}", JsonConvert.SerializeObject(responseData), exception.Message);
        
        await httpResponseWriter.WriteAsJsonAsync(httpResponse, responseData, cancellationToken);
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case ValidateException:
            case JsonPatchException:
                await WriteResponseAsync(httpContext.Response, StatusCodes.Status400BadRequest, new ResponseData<object>(data: null, errorMessage: exception.Message), exception, cancellationToken);
                break;
            case NotFoundException ex:
                await WriteResponseAsync(httpContext.Response, StatusCodes.Status404NotFound, new ResponseData<Guid>(data: ex.Id, errorMessage: exception.Message), exception, cancellationToken);
                break;
            case ConflictException ex:
                await WriteResponseAsync(httpContext.Response, StatusCodes.Status409Conflict, new ResponseData<Guid>(data: ex.Id, errorMessage: exception.Message), exception, cancellationToken);
                break;
            default:
                await WriteResponseAsync(httpContext.Response, StatusCodes.Status500InternalServerError, new ResponseData<object>(data: null, errorMessage: "Internal server error."), exception, cancellationToken);
                break;
        }

        return true;
    }
}