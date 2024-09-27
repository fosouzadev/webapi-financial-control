using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class BankAccountAppService
    (IBankAccountFactory factory, IBankAccountRepository repository)
    : IBankAccountAppService
{
    public async Task<Guid> AddAsync(AddBankAccountDto dto)
    {
        BankAccount entity = factory.CreateEntity(dto.Name, dto.Description, (byte)dto.Type);
        await repository.AddAsync(entity);

        return entity.Id;
    }

    public async Task<GetBankAccountDto> GetByIdAsync(Guid id)
    {
        BankAccount entity = await repository.GetByIdOrThrowAsync(id);

        return new()
        {
            Id = entity.Id,
            Name = entity.Name.Value,
            Description = entity.Description,
            Balance = entity.Balance,
            IsActive = entity.IsActive,
            Type = entity.Type,
            CreationDateTime = entity.CreationDateTime
        };
    }

    public async Task UpdateAsync(Guid id, JsonPatchDocument<UpdateBankAccountDto> jsonPathDocument)
    {
        BankAccount entity = await repository.GetByIdOrThrowAsync(id);

        UpdateBankAccountDto dto = new()
        {
            Name = entity.Name.Value,
            Description = entity.Description,
            IsActive = entity.IsActive
        };
        jsonPathDocument.ApplyTo(dto);

        entity.Name = new Name(dto.Name);
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;

        await repository.UpdateAsync(entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        _ = await repository.GetByIdOrThrowAsync(id);

        await repository.RemoveAsync(id);
    }
}