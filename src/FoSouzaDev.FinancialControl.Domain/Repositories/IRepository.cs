using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    Task AddAsync(T entity);
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdOrThrowAsync(Guid id);
    Task UpdateAsync(T entity);
    Task RemoveAsync(Guid id);
}