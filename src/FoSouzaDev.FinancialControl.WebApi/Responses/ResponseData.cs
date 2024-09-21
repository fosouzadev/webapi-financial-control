namespace FoSouzaDev.FinancialControl.WebApi.Responses;

public sealed class ResponseData<T>(T data, string errorMessage = null)
{
    public T Data { get; private init; } = data;
    public string ErrorMessage { get; private init; } = errorMessage;
}