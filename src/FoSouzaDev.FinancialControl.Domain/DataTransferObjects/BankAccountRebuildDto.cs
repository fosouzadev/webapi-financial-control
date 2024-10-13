namespace FoSouzaDev.FinancialControl.Domain.DataTransferObjects;

public sealed record BankAccountRebuildDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required bool IsActive { get; init; }
    public required decimal Balance { get; init; }
    public required byte Type { get; init; }
    public required DateTimeOffset CreationDateTime { get; init; }
}