using FoSouzaDev.FinancialControl.WebApi.Responses;

namespace FoSouzaDev.FinancialControl.WebApi.Settings;

public class HttpResponseWriter : IHttpResponseWriter
{
    public Task WriteAsJsonAsync<T>(HttpResponse httpResponse, ResponseData<T> responseData, CancellationToken cancellationToken) =>
        httpResponse.WriteAsJsonAsync(responseData, cancellationToken);
}