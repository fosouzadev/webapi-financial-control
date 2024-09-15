using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class FinancialMovementCategoryRepository : IFinancialMovementCategoryRepository
{
    private readonly Dictionary<Guid, List<FinancialMovementCategory>> _financialMovementCategories = new();

    public async Task AddAsync(FinancialMovementCategory entity)
    {
        if (_financialMovementCategories.TryGetValue(userId, out var financialMovementCategories))
            financialMovementCategories.Add(entity);
    }

    public async Task<FinancialMovementCategory> GetByIdAsync(Guid id)
    {
        if (_financialMovementCategories.TryGetValue(userId, out var financialMovementCategories))
            return financialMovementCategories.SingleOrDefault(a => a.Id == id);

        return null;
    }

    public async Task UpdateAsync(FinancialMovementCategory entity)
    {
        if (_financialMovementCategories.TryGetValue(userId, out var financialMovementCategories))
        {
            financialMovementCategories.Remove(entity);
            financialMovementCategories.Add(entity);
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        if (_financialMovementCategories.TryGetValue(userId, out var financialMovementCategories))
        {
            FinancialMovementCategory entity = financialMovementCategories.Single(a => a.Id == id);
            financialMovementCategories.Remove(entity);
        }
    }
}