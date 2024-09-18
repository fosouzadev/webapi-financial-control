using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories.Generics;

public interface IRemoveRepository<T> where T : Entity
{
    Task RemoveAsync(Guid id);
}