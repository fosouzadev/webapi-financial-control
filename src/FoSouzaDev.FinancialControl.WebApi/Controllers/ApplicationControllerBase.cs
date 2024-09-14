using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[ApiController]
[Authorize(Policy = "MicrosoftIdentityPolicy", Roles = "full,query")]
public abstract class ApplicationControllerBase<TEntity, TDto, TUpdateDto, TAddDto>
    (IAppServiceBase<TEntity, TDto, TUpdateDto, TAddDto> appService) : ControllerBase
    where TEntity : Entity
    where TDto : class
    where TUpdateDto : class
    where TAddDto : class
{
    private const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    protected Guid UserId => new(base.User.Claims.Single(a => a.Type == ObjectIdentifier).Value);

    [HttpPost]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> AddAsync(TAddDto dto)
    {
        Guid id = await appService.AddAsync(UserId, dto);
        return TypedResults.Created(uri: (string?)null, new ResponseData<Guid>(data: id));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetByIdAsync([FromRoute] Guid id)
    {
        TDto dto = await appService.GetByIdAsync(UserId, id);
        return TypedResults.Ok(new ResponseData<TDto>(dto));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> UpdateAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<TUpdateDto> pathDocument)
    {
        await appService.UpdateAsync(UserId, id, pathDocument);
        return TypedResults.NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> RemoveAsync([FromRoute] Guid id)
    {
        await appService.RemoveAsync(UserId, id);
        return TypedResults.NoContent();
    }
}