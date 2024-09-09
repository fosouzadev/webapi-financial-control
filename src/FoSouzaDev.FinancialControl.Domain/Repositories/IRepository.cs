using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    Task AddAsync(Guid userId, T entity);
    Task<T> GetByIdAsync(Guid userId, Guid id);
    Task UpdateAsync(Guid userId, T entity);
    Task RemoveAsync(Guid userId, Guid id);
}