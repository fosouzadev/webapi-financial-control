using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class BankAccountFactory : FactoryBase, IBankAccountFactory
{
    public Task<BankAccount> CreateEntityAsync(BankAccountCreateDto dto) =>
        RebuildEntityAsync(new BankAccountRebuildDto
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true,
            Balance = 0,
            Type = dto.Type,
            CreationDateTime = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid()
        });

    public Task<BankAccount> RebuildEntityAsync(BankAccountRebuildDto dto)
    {
        base.ThrowIfIsNotValidValue<BankAccountType>(dto.Type);

        return Task.FromResult(new BankAccount(
            new Name(dto.Name),
            dto.Description,
            dto.IsActive,
            (BankAccountType)dto.Type,
            dto.Balance,
            dto.CreationDateTime,
            dto.Id));
    }
}