using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Application.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services;

public abstract class AppService<TEntity, TDto, TUpdateDto, TAddDto>
    (IAppFactory<TEntity, TDto, TUpdateDto, TAddDto> factory, IRepository<TEntity> repository)
    : IAppService<TEntity, TDto, TUpdateDto, TAddDto>
    where TEntity : Entity
    where TDto : class
    where TUpdateDto : class
    where TAddDto : class
{
    public async Task<Guid> AddAsync(Guid userId, TAddDto dto)
    {
        TEntity entity = factory.AddDtoToDomainEntity(dto);
        await repository.AddAsync(userId, entity);

        return entity.Id;
    }

    public async Task<TDto> GetByIdAsync(Guid userId, Guid id)
    {
        TEntity entity = await GetByIdOrThrowAsync(userId, id);
        return factory.DomainEntityToDto(entity);
    }

    public async Task UpdateAsync(Guid userId, Guid id, JsonPatchDocument<TUpdateDto> pathDocument)
    {
        TEntity entity = await GetByIdOrThrowAsync(userId, id);

        TUpdateDto dto = factory.DomainEntityToUpdateDto(entity);
        pathDocument.ApplyTo(dto);

        UpdateEntity(entity, dto);

        await repository.UpdateAsync(userId, entity);
    }

    public async Task RemoveAsync(Guid userId, Guid id)
    {
        _ = await GetByIdOrThrowAsync(userId, id);

        await repository.RemoveAsync(userId, id);
    }

    protected abstract void UpdateEntity(TEntity entity, TUpdateDto dto);

    private async Task<TEntity> GetByIdOrThrowAsync(Guid userId, Guid id) =>
        (await repository.GetByIdAsync(userId, id)) ?? throw new NotFoundException(id);
}