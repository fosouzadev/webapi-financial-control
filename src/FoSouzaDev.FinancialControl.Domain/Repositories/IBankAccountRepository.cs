using FoSouzaDev.Common.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IBankAccountRepository :
    IAddRepository<BankAccount>,
    IGetRepository<BankAccount>,
    IUpdateRepository<BankAccount>,
    IRemoveRepository<BankAccount>
{
}