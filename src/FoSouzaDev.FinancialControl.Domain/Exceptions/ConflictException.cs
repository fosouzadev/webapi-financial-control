namespace FoSouzaDev.FinancialControl.Domain.Exceptions;

public sealed class ConflictException(string identifier) : Exception(message: "Already registered.")
{
    public string Identifier => identifier;
}