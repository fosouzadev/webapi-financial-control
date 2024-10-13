using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class FinancialMovementFactory
    (IBankAccountRepository bankRepository, IFinancialMovementCategoryRepository categoryRepository)
    : FactoryBase, IFinancialMovementFactory
{
    public async Task<FinancialMovement> CreateEntityAsync(string name, decimal amount, byte type, Guid categoryId, Guid bankAccountId) =>
        await RebuildEntityAsync(name, amount, type, categoryId, bankAccountId, DateTimeOffset.UtcNow, Guid.NewGuid());

    public async Task<FinancialMovement> RebuildEntityAsync(
        string name, decimal amount, byte type, Guid categoryId, Guid bankAccountId, DateTimeOffset creationDateTime, Guid id)
    {
        base.ThrowIfIsNotValidValue<FinancialMovementType>(type);

        return new(
            new Name(name),
            new Amount(amount),
            (FinancialMovementType)type,
            await categoryRepository.GetByIdOrThrowAsync(categoryId),
            await bankRepository.GetByIdOrThrowAsync(bankAccountId),
            creationDateTime,
            id);
    }
}