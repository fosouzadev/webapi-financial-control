namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record FinancialMovementCategoryDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}