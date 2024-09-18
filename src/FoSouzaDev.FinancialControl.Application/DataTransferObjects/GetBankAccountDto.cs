using FoSouzaDev.FinancialControl.Domain.Enums;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record GetBankAccountDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required bool IsActive { get; init; }
    public required decimal Balance { get; init; }
    public required BankAccountType Type { get; init; }
    public required DateTimeOffset CreationDateTime { get; init; }
}