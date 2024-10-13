namespace FoSouzaDev.FinancialControl.Domain.DataTransferObjects;

public sealed record FinancialMovementCategoryCreateDto
{
    public required string Name { get; init; }
}