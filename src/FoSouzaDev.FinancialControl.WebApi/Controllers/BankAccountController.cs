using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[Route("api/v1/bank-account")]
public class BankAccountController(IBankAccountAppService appService)
    : ApplicationControllerBase<BankAccount, BankAccountDto, UpdateBankAccountDto, AddBankAccountDto>(appService)
{
    [HttpPost("{id}/financial-movement")]
    public async Task<IResult> AddFinancialMovementAsync([FromRoute] Guid bankAccountId, [FromBody] AddFinancialMovementDto dto)
    {
        Guid id = await appService.AddFinancialMovementAsync(UserId, bankAccountId, dto);
        return TypedResults.Created(uri: (string?)null, new ResponseData<Guid>(data: id));
    }
}