namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record AddFinancialMovementCategoryDto
{
    public required string Name { get; init; }
}