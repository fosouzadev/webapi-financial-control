using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces.Base;

public interface IPartialActionsAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    Task<Guid> AddAsync(TAddDto dto);
    Task<TGetDto> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, JsonPatchDocument<TUpdateDto> jsonPathDocument);
}