using FoSouzaDev.FinancialControl.Application.Services.Interfaces.Base;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers.Base;

public abstract class PartialActionsControllerBase<TAppService, TAddDto, TGetDto, TUpdateDto> : ApplicationControllerBase
    where TAppService : IPartialActionsAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    protected TAppService AppService { get; set; }

    protected PartialActionsControllerBase(TAppService appService)
    {
        AppService = appService;
    }

    [HttpPost]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> AddAsync([FromBody] TAddDto request)
    {
        Guid id = await AppService.AddAsync(request);
        return TypedResults.Created(uri: Url.Action(nameof(GetByIdAsync), new { id }), new ResponseData<Guid>(data: id));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(ResponseData<TGetDto>), StatusCodes.Status200OK)] // C# não permite usar tipo genérico no atributo
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IResult> GetByIdAsync([FromRoute] Guid id)
    {
        TGetDto data = await AppService.GetByIdAsync(id);
        return TypedResults.Ok(new ResponseData<TGetDto>(data));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType<ResponseData>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> UpdateAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<TUpdateDto> jsonPathDocument)
    {
        await AppService.UpdateAsync(id, jsonPathDocument);
        return TypedResults.NoContent();
    }
}