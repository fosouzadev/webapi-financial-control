using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class FinancialMovementAppService
    (IFinancialMovementFactory factory,
     IFinancialMovementRepository repository,
     IBankAccountRepository bankRepository,
     IFinancialMovementCategoryRepository categoryRepository)
    : IFinancialMovementAppService
{
    public async Task<Guid> AddAsync(AddFinancialMovementDto dto)
    {
        BankAccount bankAccount = await bankRepository.GetByIdOrThrowAsync(dto.BankAccountId);
        FinancialMovementCategory category = await categoryRepository.GetByIdOrThrowAsync(dto.CategoryId);

        FinancialMovement financialMovement = factory.CreateEntityAsync(
            dto.Name, dto.Amount, (Domain.Enums.FinancialMovementType)dto.Type, category, bankAccount);

        await repository.AddAsync(financialMovement);

        return financialMovement.Id;
    }

    public async Task<FinancialMovementDto> GetByIdAsync(Guid financialMovementId)
    {
        FinancialMovement financialMovement = await repository.GetByIdOrThrowAsync(financialMovementId);
        
        return new()
        {
            Id = financialMovement.Id,
            Name = financialMovement.Name.Value,
            Amount = financialMovement.Amount.Value,
            Type = (DataTransferObjects.Enums.FinancialMovementType)financialMovement.Type,
            CategoryId = financialMovement.Category.Id,
            BankAccountId = financialMovement.BankAccount.Id,
            CreationDateTime = financialMovement.CreationDateTime
        };
    }

    public async Task UpdateAsync(Guid financialMovementId, JsonPatchDocument<UpdateFinancialMovementDto> pathDocument)
    {
        FinancialMovement entity = await repository.GetByIdOrThrowAsync(financialMovementId);

        UpdateFinancialMovementDto dto = new()
        {
            Name = entity.Name.Value,
            Amount = entity.Amount.Value,
            CategoryId = entity.Category.Id
        };
        pathDocument.ApplyTo(dto);

        FinancialMovementCategory category = await categoryRepository.GetByIdOrThrowAsync(dto.CategoryId);

        entity.Name = new Name(dto.Name);
        entity.Amount = new Amount(dto.Amount);
        entity.Category = category;

        await repository.UpdateAsync(entity);
    }
}