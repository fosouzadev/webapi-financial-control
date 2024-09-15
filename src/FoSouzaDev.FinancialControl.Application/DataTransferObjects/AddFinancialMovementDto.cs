using FoSouzaDev.FinancialControl.Application.Enums;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record AddFinancialMovementDto
{
    public required string Name { get; init; }
    public required decimal Amount { get; init; }
    public required FinancialMovementType Type { get; init; }
    public required Guid CategoryId { get; init; }
}