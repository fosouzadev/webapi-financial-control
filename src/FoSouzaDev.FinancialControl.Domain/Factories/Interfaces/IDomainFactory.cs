using FoSouzaDev.Common.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IDomainFactory<TEntity, TCreateDto, TRebuildDto>
    where TEntity : Entity
    where TCreateDto : class
    where TRebuildDto : class
{
    /// <summary>
    /// Utilizado pela camada de aplicação para criar a entidade com os dados da requisição
    /// </summary>
    TEntity CreateEntity(TCreateDto dto);

    /// <summary>
    /// Utilizado pela camada de Infrastructure para recriar a entidade com os dados do banco de dados
    /// </summary>
    TEntity RebuildEntity(TRebuildDto dto);
}