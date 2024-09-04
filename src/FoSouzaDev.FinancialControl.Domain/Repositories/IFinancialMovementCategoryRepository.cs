using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IFinancialMovementCategoryRepository
{
    Task Add(FinancialMovementCategory category);
    Task<FinancialMovementCategory> GetById(Guid id);
    Task Update(FinancialMovementCategory category);
    Task Delete(Guid id);
}