namespace FoSouzaDev.FinancialControl.Domain.DataTransferObjects;

public sealed record BankAccountCreateDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required byte Type { get; init; }
}