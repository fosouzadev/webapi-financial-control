using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Enums;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class FinancialMovementAppService
    (IFinancialMovementFactory factory, IFinancialMovementRepository repository, IFinancialMovementCategoryRepository categoryRepository)
    : IFinancialMovementAppService
{
    public async Task<Guid> AddAsync(AddFinancialMovementDto dto)
    {
        FinancialMovement financialMovement = await factory.CreateEntityAsync(dto.Name, dto.Amount, (byte)dto.Type, dto.CategoryId, dto.BankAccountId);
        await repository.AddAsync(financialMovement);

        return financialMovement.Id;
    }

    public async Task<GetFinancialMovementDto> GetByIdAsync(Guid financialMovementId)
    {
        FinancialMovement financialMovement = await repository.GetByIdOrThrowAsync(financialMovementId);
        
        return new()
        {
            Id = financialMovement.Id,
            Name = financialMovement.Name.Value,
            Amount = financialMovement.Amount.Value,
            Type = (FinancialMovementTypeEnum)financialMovement.Type,
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

        // TODO: verificar se é possível melhorar isso
        FinancialMovementCategory category = await categoryRepository.GetByIdOrThrowAsync(dto.CategoryId);

        entity.Name = new Name(dto.Name);
        entity.Amount = new Amount(dto.Amount);
        entity.Category = category;

        await repository.UpdateAsync(entity);
    }
}