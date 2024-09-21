using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class FinancialMovementCategoryAppService
    (IFinancialMovementCategoryFactory factory, IFinancialMovementCategoryRepository repository)
    : IFinancialMovementCategoryAppService
{
    public Guid Add(AddFinancialMovementCategoryDto dto)
    {
        FinancialMovementCategory entity = factory.CreateEntity(dto.Name);
        repository.Add(entity);

        return entity.Id;
    }

    public GetFinancialMovementCategoryDto GetById(Guid id)
    {
        FinancialMovementCategory entity = repository.GetByIdOrThrow(id);
        return new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            CreationDateTime = entity.CreationDateTime
        };
    }

    public async Task UpdateAsync(Guid id, JsonPatchDocument<UpdateFinancialMovementCategoryDto> pathDocument)
    {
        FinancialMovementCategory entity = repository.GetByIdOrThrow(id);

        UpdateFinancialMovementCategoryDto dto = new() { Name = entity.Name.Value };
        pathDocument.ApplyTo(dto);

        entity.Name = new Name(dto.Name);

        await repository.UpdateAsync(entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        _ = repository.GetByIdOrThrow(id);

        await repository.RemoveAsync(id);
    }
}