using FoSouzaDev.FinancialControl.Application.Services.Interfaces.Base;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers.Base;

public abstract class AllActionsControllerBase<TAppService, TAddDto, TGetDto, TUpdateDto>
    : PartialActionsControllerBase<TAppService, TAddDto, TGetDto, TUpdateDto>
    where TAppService : IAllActionsAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    protected AllActionsControllerBase(TAppService appService) : base(appService)
    {
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> RemoveAsync([FromRoute] Guid id)
    {
        await base.AppService.RemoveAsync(id);
        return TypedResults.NoContent();
    }
}