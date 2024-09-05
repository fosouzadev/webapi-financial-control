using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class FinancialMovementCategoryRepository : IFinancialMovementCategoryRepository
{
    private readonly List<FinancialMovementCategory> _financialMovementCategories = new();

    public async Task AddAsync(Guid userId, FinancialMovementCategory category)
    {
        _financialMovementCategories.Add(category);
    }

    public async Task<FinancialMovementCategory> GetByIdAsync(Guid userId, Guid id)
    {
        return _financialMovementCategories.SingleOrDefault(c => c.Id == id);
    }

    public async Task UpdateAsync(Guid userId, FinancialMovementCategory category)
    {
        _financialMovementCategories.Remove(category);
        _financialMovementCategories.Add(category);
    }

    public async Task RemoveAsync(Guid userId, Guid id)
    {
        FinancialMovementCategory category = await GetByIdAsync(userId, id);
        _financialMovementCategories.Remove(category);
    }
}