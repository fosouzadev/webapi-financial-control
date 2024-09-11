namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record UpdateFinancialMovementCategoryDto
{
    public required string Name { get; set; }
}