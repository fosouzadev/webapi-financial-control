using FoSouzaDev.FinancialControl.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IAppServiceBase<TEntity, TDto, TUpdateDto, TAddDto>
    where TEntity : Entity
    where TDto : class
    where TUpdateDto : class
    where TAddDto : class
{
    Task<Guid> AddAsync(Guid userId, TAddDto dto);
    Task<TDto> GetByIdAsync(Guid userId, Guid id);
    Task UpdateAsync(Guid userId, Guid id, JsonPatchDocument<TUpdateDto> pathDocument);
    Task RemoveAsync(Guid userId, Guid id);
}