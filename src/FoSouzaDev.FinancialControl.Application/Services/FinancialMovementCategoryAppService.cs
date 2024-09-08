using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class FinancialMovementCategoryAppService
    (IFinancialMovementCategoryRepository repository) : IFinancialMovementCategoryAppService
{
    public async Task<Guid> AddAsync(Guid userId, AddOrUpdateFinancialMovementCategoryDto dto)
    {
        FinancialMovementCategory entity = (FinancialMovementCategory)dto;
        await repository.AddAsync(userId, entity);

        return entity.Id;
    }

    public async Task<FinancialMovementCategoryDto> GetByIdAsync(Guid userId, Guid id)
    {
        FinancialMovementCategory entity = await GetByIdOrThrowAsync(userId, id);
        return (FinancialMovementCategoryDto)entity;
    }

    public async Task UpdateAsync(Guid userId, Guid id, JsonPatchDocument<AddOrUpdateFinancialMovementCategoryDto> pathDocument)
    {
        FinancialMovementCategory entity = await GetByIdOrThrowAsync(userId, id);

        AddOrUpdateFinancialMovementCategoryDto dto = (AddOrUpdateFinancialMovementCategoryDto)entity;
        pathDocument.ApplyTo(dto);

        entity.Name = new Name(dto.Name);

        await repository.UpdateAsync(userId, entity);
    }

    public async Task RemoveAsync(Guid userId, Guid id)
    {
        _ = await GetByIdOrThrowAsync(userId, id);

        await repository.RemoveAsync(userId, id);
    }

    private async Task<FinancialMovementCategory> GetByIdOrThrowAsync(Guid userId, Guid id) =>
        (await repository.GetByIdAsync(userId, id)) ?? throw new NotFoundException(id);
}