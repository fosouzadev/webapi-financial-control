using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal sealed class BankAccountAppService
    : AppService<BankAccount, BankAccountDto, UpdateBankAccountDto, AddBankAccountDto>, IBankAccountAppService
{
    public BankAccountAppService(IBankAccountFactory factory, IBankAccountRepository repository)
        : base(factory, repository)
    {
    }

    protected override void UpdateEntity(BankAccount entity, UpdateBankAccountDto dto)
    {
        throw new NotImplementedException();
    }
}