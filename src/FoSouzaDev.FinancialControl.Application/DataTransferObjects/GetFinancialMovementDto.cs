using FoSouzaDev.FinancialControl.Application.Enums;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record GetFinancialMovementDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required decimal Amount { get; init; }
    public required FinancialMovementTypeEnum Type { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateTimeOffset CreationDateTime { get; init; }
    public required Guid BankAccountId { get; init; }
}