namespace FoSouzaDev.FinancialControl.WebApi.Responses;

public sealed class ResponseData<T>(T data, string errorMessage = null) : ResponseBase(errorMessage)
{
    public T Data { get; private init; } = data ??
        throw new ArgumentNullException(paramName: nameof(data), message: "The parameter cannot be null.");
}

public sealed class ResponseData(string errorMessage) : ResponseBase(errorMessage)
{
}