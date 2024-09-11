using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[Route("api/v1/bank-account")]
public class BankAccountController(IBankAccountAppService appService)
    : ApplicationControllerBase<BankAccount, BankAccountDto, UpdateBankAccountDto, AddBankAccountDto>(appService)
{
}