﻿using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
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
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> AddAsync(AddBankAccountDto dto)
    {
        Guid id = await appService.AddAsync(dto);
        return TypedResults.Created(uri: (string?)null, new ResponseData<Guid>(data: id));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> GetByIdAsync([FromRoute] Guid id)
    {
        BankAccountDto dto = await appService.GetByIdAsync(id);
        return TypedResults.Ok(new ResponseData<BankAccountDto>(dto));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> UpdateAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<UpdateBankAccountDto> pathDocument)
    {
        await appService.UpdateAsync(id, pathDocument);
        return TypedResults.NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseData<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> RemoveAsync([FromRoute] Guid id)
    {
        await appService.RemoveAsync(id);
        return TypedResults.NoContent();
    }
}