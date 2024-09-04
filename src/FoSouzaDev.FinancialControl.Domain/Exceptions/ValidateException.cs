namespace FoSouzaDev.FinancialControl.Domain.Exceptions;

public sealed class ValidateException(string message) : Exception(message)
{
}