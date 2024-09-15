using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class BankAccountRepository : IBankAccountRepository
{
    private readonly Dictionary<Guid, List<BankAccount>> _bankAccounts = new();

    public async Task AddAsync(BankAccount entity)
    {
        if (_bankAccounts.TryGetValue(userId, out var bankAccounts))
            bankAccounts.Add(entity);
        else
            _bankAccounts.Add(userId, new List<BankAccount> { entity });
    }

    public async Task<BankAccount> GetByIdAsync(Guid id)
    {
        if (_bankAccounts.TryGetValue(userId, out var bankAccounts))
            return bankAccounts.SingleOrDefault(a => a.Id == id);

        return null;
    }

    public async Task UpdateAsync(BankAccount entity)
    {
        if (_bankAccounts.TryGetValue(userId, out var bankAccounts))
        {
            bankAccounts.Remove(entity);
            bankAccounts.Add(entity);
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        if (_bankAccounts.TryGetValue(userId, out var bankAccounts))
        {
            BankAccount entity = bankAccounts.Single(a => a.Id == id);
            bankAccounts.Remove(entity);
        }
    }
}