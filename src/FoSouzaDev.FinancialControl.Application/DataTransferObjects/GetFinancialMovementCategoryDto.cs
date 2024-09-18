namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record GetFinancialMovementCategoryDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}