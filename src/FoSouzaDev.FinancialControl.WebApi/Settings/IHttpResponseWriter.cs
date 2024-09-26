using FoSouzaDev.FinancialControl.WebApi.Responses;

namespace FoSouzaDev.FinancialControl.WebApi.Settings;

public interface IHttpResponseWriter
{
    Task WriteAsJsonAsync<T>(HttpResponse httpResponse, T responseData, CancellationToken cancellationToken)
        where T : ResponseBase;
}