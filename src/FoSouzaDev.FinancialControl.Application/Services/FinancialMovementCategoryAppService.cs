using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class FinancialMovementCategoryAppService
    (IFinancialMovementCategoryFactory factory,
     IFinancialMovementCategoryRepository repository,
     IUserAppService userAppService)
    : IFinancialMovementCategoryAppService
{
    public async Task<Guid> AddAsync(FinancialMovementCategoryDto dto)
    {
        FinancialMovementCategory entity = factory.CreateEntity(dto.Name);
        await repository.AddAsync(entity);

        return entity.Id;
    }

    public async Task<Guid> AddAsync(Guid userId, TAddDto dto)
    {
        TEntity entity = factory.AddDtoToDomainEntity(dto);
        await repository.AddAsync(userId, entity);

        return entity.Id;
    }

    public async Task<TDto> GetByIdAsync(Guid userId, Guid id)
    {
        TEntity entity = await repository.GetByIdOrThrowAsync(userId, id);
        return factory.DomainEntityToDto(entity);
    }

    public async Task UpdateAsync(Guid userId, Guid id, JsonPatchDocument<TUpdateDto> pathDocument)
    {
        TEntity entity = await repository.GetByIdOrThrowAsync(userId, id);

        TUpdateDto dto = factory.DomainEntityToUpdateDto(entity);
        pathDocument.ApplyTo(dto);

        UpdateEntity(entity, dto);

        await repository.UpdateAsync(userId, entity);
    }

    public async Task RemoveAsync(Guid userId, Guid id)
    {
        _ = await repository.GetByIdOrThrowAsync(userId, id);

        await repository.RemoveAsync(userId, id);
    }

    protected override void UpdateEntity(FinancialMovementCategory entity, UpdateFinancialMovementCategoryDto dto)
    {
        entity.Name = new Name(dto.Name);
    }
}