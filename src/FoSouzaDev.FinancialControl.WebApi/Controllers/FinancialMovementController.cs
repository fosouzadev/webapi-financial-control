using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[Route("api/v1/financial-movement")]
public class FinancialMovementController(IFinancialMovementAppService appService)
    : PartialActionsControllerBase<AddFinancialMovementDto, GetFinancialMovementDto, UpdateFinancialMovementDto>(appService)
{
    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseData<GetFinancialMovementDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public override async Task<IResult> GetByIdAsync([FromRoute] Guid id) =>
        await base.GetByIdAsync(id);
}