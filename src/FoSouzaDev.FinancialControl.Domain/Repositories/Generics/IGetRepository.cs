using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories.Generics;

public interface IGetRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdOrThrowAsync(Guid id);
}