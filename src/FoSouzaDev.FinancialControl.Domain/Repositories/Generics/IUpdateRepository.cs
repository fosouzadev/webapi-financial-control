using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories.Generics;

public interface IUpdateRepository<T> where T : Entity
{
    Task UpdateAsync(T entity);
}