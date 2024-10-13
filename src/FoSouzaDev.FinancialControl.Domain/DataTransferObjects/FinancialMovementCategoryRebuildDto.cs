namespace FoSouzaDev.FinancialControl.Domain.DataTransferObjects;

public sealed record FinancialMovementCategoryRebuildDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTimeOffset CreationDateTime { get; init; }
}