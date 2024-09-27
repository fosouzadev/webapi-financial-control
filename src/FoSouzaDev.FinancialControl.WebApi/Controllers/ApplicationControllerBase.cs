using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[ApiController]
[Authorize(Policy = "MicrosoftIdentityPolicy", Roles = "full,query")]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class ApplicationControllerBase : ControllerBase
{
}

public abstract class ApplicationControllerBase<TAppService, TAddDto, TGetDto> : ControllerBase
    where TAppService : class
    where TAddDto : class
    where TGetDto : class
{
    protected TAppService AppService { get; init; }

    protected ApplicationControllerBase(TAppService appService)
    {
        AppService = appService;
    }

    [HttpPost]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> AddAsync(TAddDto request)
    {
        Guid id = await AppService.AddAsync(request);
        return TypedResults.Created(uri: Url.Action(nameof(GetByIdAsync), new { id }), new ResponseData<Guid>(data: id));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(TGetDto), StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetByIdAsync([FromRoute] Guid id)
    {
        TGetDto data = await AppService.GetByIdAsync(id);
        return TypedResults.Ok(new ResponseData<TGetDto>(data));
    }
}