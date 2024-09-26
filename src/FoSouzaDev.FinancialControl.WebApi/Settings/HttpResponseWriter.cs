using FoSouzaDev.FinancialControl.WebApi.Responses;
using System.Diagnostics.CodeAnalysis;

namespace FoSouzaDev.FinancialControl.WebApi.Settings;

[ExcludeFromCodeCoverage]
public class HttpResponseWriter : IHttpResponseWriter
{
    public Task WriteAsJsonAsync<T>(HttpResponse httpResponse, T responseData, CancellationToken cancellationToken)
        where T : ResponseBase => httpResponse.WriteAsJsonAsync(responseData, cancellationToken);
}