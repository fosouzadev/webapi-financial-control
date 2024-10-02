using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[ApiController]
[Authorize(Policy = "MicrosoftIdentityPolicy", Roles = "full,query")]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class ApplicationControllerBase<TAddDto, TGetDto, TUpdateDto>
    (IAppService<TAddDto, TGetDto, TUpdateDto> appService) : ControllerBase
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    [HttpPost]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> AddAsync([FromBody] TAddDto request)
    {
        Guid id = await appService.AddAsync(request);
        return TypedResults.Created(uri: Url.Action(nameof(GetByIdAsync), new { id }), new ResponseData<Guid>(data: id));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(ResponseData<TGetDto>), StatusCodes.Status200OK)] // C# não permite usar tipo genérico no atributo
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IResult> GetByIdAsync([FromRoute] Guid id)
    {
        TGetDto data = await appService.GetByIdAsync(id);
        return TypedResults.Ok(new ResponseData<TGetDto>(data));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType<ResponseData>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> UpdateAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<TUpdateDto> jsonPathDocument)
    {
        await appService.UpdateAsync(id, jsonPathDocument);
        return TypedResults.NoContent();
    }
}

public abstract class PartialActionsControllerBase<TAddDto, TGetDto, TUpdateDto>
    (IPartialActionsAppService<TAddDto, TGetDto, TUpdateDto> appService) : ApplicationControllerBase<TAddDto, TGetDto, TUpdateDto>(appService)
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    protected IPartialActionsAppService<TAddDto, TGetDto, TUpdateDto> AppService { get; init; } = appService;
}

public abstract class AllActionsControllerBase<TAddDto, TGetDto, TUpdateDto>
    (IAllActionsAppService<TAddDto, TGetDto, TUpdateDto> appService) : ApplicationControllerBase<TAddDto, TGetDto, TUpdateDto>(appService)
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    protected IAllActionsAppService<TAddDto, TGetDto, TUpdateDto> AppService { get; init; } = appService;

    [HttpDelete("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> RemoveAsync([FromRoute] Guid id)
    {
        await AppService.RemoveAsync(id);
        return TypedResults.NoContent();
    }
}