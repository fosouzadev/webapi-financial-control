using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Application.Factories.Interfaces;

public interface IAppFactory<TEntity, TDto, TUpdateDto, TAddDto>
    where TEntity : Entity
    where TDto : class
    where TUpdateDto : class
    where TAddDto : class
{
    TDto DomainEntityToDto(TEntity entity);
    TUpdateDto DomainEntityToUpdateDto(TEntity entity);
    TEntity AddDtoToDomainEntity(TAddDto dto);
}