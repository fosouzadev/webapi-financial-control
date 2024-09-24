namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories.Generic;

internal interface IGenericRepository<TDataEntity>
{
    Task AddAsync(TDataEntity dataEntity);
    Task<TDataEntity> GetByIdAsync(Guid id);
    Task<TDataEntity> GetByIdOrThrowAsync(Guid id);
    Task UpdateAsync(Guid id, Action<TDataEntity> setChanges);
    Task RemoveAsync(Guid id);
}