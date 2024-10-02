using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[Route("api/v1/financial-movement-category")]
public sealed class FinancialMovementCategoryController(IFinancialMovementCategoryAppService appService)
    : AllActionsControllerBase<AddFinancialMovementCategoryDto, GetFinancialMovementCategoryDto, UpdateFinancialMovementCategoryDto>(appService)
{
    [HttpGet("{id}")]
    [ProducesResponseType<ResponseData<Guid>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseData<GetFinancialMovementCategoryDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseData>(StatusCodes.Status500InternalServerError)]
    public override async Task<IResult> GetByIdAsync([FromRoute] Guid id) =>
        await base.GetByIdAsync(id);
}