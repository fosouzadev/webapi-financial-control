using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class BankAccountRepository(IUserService service) : IBankAccountRepository
{
    private readonly Dictionary<Guid, List<BankAccount>> _bankAccounts = new();

    public async Task AddAsync(BankAccount entity)
    {
        if (_bankAccounts.TryGetValue(service.GetUserId(), out var bankAccounts))
            bankAccounts.Add(entity);
        else
            _bankAccounts.Add(service.GetUserId(), new List<BankAccount> { entity });
    }

    public async Task<BankAccount> GetByIdAsync(Guid id)
    {
        if (_bankAccounts.TryGetValue(service.GetUserId(), out var bankAccounts))
            return bankAccounts.SingleOrDefault(a => a.Id == id);

        return null;
    }

    public async Task<BankAccount> GetByIdOrThrowAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            throw new NotFoundException(id);

        return entity;
    }

    public async Task UpdateAsync(BankAccount entity)
    {
        if (_bankAccounts.TryGetValue(service.GetUserId(), out var bankAccounts))
        {
            bankAccounts.Remove(entity);
            bankAccounts.Add(entity);
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        if (_bankAccounts.TryGetValue(service.GetUserId(), out var bankAccounts))
        {
            BankAccount entity = bankAccounts.Single(a => a.Id == id);
            bankAccounts.Remove(entity);
        }
    }

    public async Task AddFinancialMovementAsync(FinancialMovement financialMovement)
    {
        throw new NotImplementedException();
    }

    public async Task<FinancialMovement> GetFinancialMovementByIdOrThrowAsync(Guid bankAccountId, Guid financialMovementId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateFinancialMovementAsync(FinancialMovement financialMovement)
    {
        throw new NotImplementedException();
    }
}