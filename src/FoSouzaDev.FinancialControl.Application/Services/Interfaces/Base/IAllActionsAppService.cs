namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces.Base;

public interface IAllActionsAppService<TAddDto, TGetDto, TUpdateDto> : IPartialActionsAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    Task RemoveAsync(Guid id);
}