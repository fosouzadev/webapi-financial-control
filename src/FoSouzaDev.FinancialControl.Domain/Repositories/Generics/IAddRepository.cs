using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories.Generics;

public interface IAddRepository<T> where T : Entity
{
    Task AddAsync(T entity);
}