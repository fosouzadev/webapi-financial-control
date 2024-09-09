using FoSouzaDev.FinancialControl.Application.Factories;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record UpdateBankAccount
{
    public required string Name { get; set; }
    public string Description { get; set; }
    public required bool IsActive { get; set; }

    public static explicit operator UpdateBankAccount(BankAccount entity) =>
        BankAccountFactory.DomainEntityToUpdateDto(entity);
}