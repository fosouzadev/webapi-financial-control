using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Controllers.Base;
using FoSouzaDev.FinancialControl.WebApi.Controllers;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Controllers;

public class BankAccountControllerTest : AllActionsControllerBaseTest<BankAccountController, IBankAccountAppService, AddBankAccountDto, GetBankAccountDto, UpdateBankAccountDto>
{
}