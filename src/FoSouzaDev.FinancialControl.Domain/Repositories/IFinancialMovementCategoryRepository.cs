using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories.Generics;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IFinancialMovementCategoryRepository :
    IAddRepository<FinancialMovementCategory>,
    IGetRepository<FinancialMovementCategory>,
    IUpdateRepository<FinancialMovementCategory>,
    IRemoveRepository<FinancialMovementCategory>
{
}