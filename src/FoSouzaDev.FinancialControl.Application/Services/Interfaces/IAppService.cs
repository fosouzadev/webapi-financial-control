using Microsoft.AspNetCore.JsonPatch;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    Task<Guid> AddAsync(TAddDto dto);
    Task<TGetDto> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, JsonPatchDocument<TUpdateDto> jsonPathDocument);
}

public interface IAllActionsAppService<TAddDto, TGetDto, TUpdateDto> : IAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    Task RemoveAsync(Guid id);
}

public interface IPartialActionsAppService<TAddDto, TGetDto, TUpdateDto> : IAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
}