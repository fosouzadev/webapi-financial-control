using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[Route("api/v1/bank-account")]
public class BankAccountController(IBankAccountAppService appService) : ApplicationControllerBase
{
    [HttpPost]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> AddAsync(AddBankAccountDto request)
    {
        Guid id = await appService.AddAsync(request);
        return TypedResults.Created(uri: Url.Action(nameof(GetByIdAsync), new { id }), new ResponseData<Guid>(data: id));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseData<GetBankAccountDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetByIdAsync([FromRoute] Guid id)
    {
        GetBankAccountDto data = await appService.GetByIdAsync(id);
        return TypedResults.Ok(new ResponseData<GetBankAccountDto>(data));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType<ResponseData>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> UpdateAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<UpdateBankAccountDto> jsonPathDocument)
    {
        await appService.UpdateAsync(id, jsonPathDocument);
        return TypedResults.NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> RemoveAsync([FromRoute] Guid id)
    {
        await appService.RemoveAsync(id);
        return TypedResults.NoContent();
    }
}