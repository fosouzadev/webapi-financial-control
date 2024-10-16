﻿using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
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
    public async Task<Guid> AddAsync(AddFinancialMovementCategoryDto dto)
    {
        FinancialMovementCategory entity = await factory.CreateEntityAsync(new FinancialMovementCategoryCreateDto { Name = dto.Name });
        await repository.AddAsync(entity);

        return entity.Id;
    }

    public async Task<GetFinancialMovementCategoryDto> GetByIdAsync(Guid id)
    {
        FinancialMovementCategory entity = await repository.GetByIdOrThrowAsync(id);

        return new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            CreationDateTime = entity.CreationDateTime
        };
    }

    public async Task UpdateAsync(Guid id, JsonPatchDocument<UpdateFinancialMovementCategoryDto> jsonPathDocument)
    {
        FinancialMovementCategory entity = await repository.GetByIdOrThrowAsync(id);

        UpdateFinancialMovementCategoryDto dto = new() { Name = entity.Name.Value };
        jsonPathDocument.ApplyTo(dto);

        entity.Name = new Name(dto.Name);

        await repository.UpdateAsync(entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        _ = repository.GetByIdOrThrowAsync(id);

        await repository.RemoveAsync(id);
    }
}