using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal sealed class FinancialMovementCategoryRepository : IFinancialMovementCategoryRepository
{
    private readonly List<FinancialMovementCategory> _financialMovementCategories = new();

    public async Task Add(FinancialMovementCategory category)
    {
        _financialMovementCategories.Add(category);
    }

    public async Task<FinancialMovementCategory> GetById(Guid id)
    {
        return _financialMovementCategories.SingleOrDefault(c => c.Id == id);
    }

    public async Task Update(FinancialMovementCategory category)
    {
        _financialMovementCategories.Remove(category);
        _financialMovementCategories.Add(category);
    }

    public async Task Delete(Guid id)
    {
        FinancialMovementCategory category = await GetById(id);
        _financialMovementCategories.Remove(category);
    }
}