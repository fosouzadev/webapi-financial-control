using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IFinancialMovementCategoryRepository
{
    Task AddAsync(Guid userId, FinancialMovementCategory category);
    Task<FinancialMovementCategory> GetByIdAsync(Guid userId, Guid id);
    Task UpdateAsync(Guid userId, FinancialMovementCategory category);
    Task RemoveAsync(Guid userId, Guid id);
}