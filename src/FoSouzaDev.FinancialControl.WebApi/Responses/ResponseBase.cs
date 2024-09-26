namespace FoSouzaDev.FinancialControl.WebApi.Responses;

public abstract class ResponseBase(string errorMessage)
{
    public string ErrorMessage { get; private init; } = errorMessage;
}