using FoSouzaDev.Common.Domain.Repositories;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IFinancialMovementCategoryRepository : 
    IAddRepository<FinancialMovementCategory>,
    IGetRepository<FinancialMovementCategory>,
    IUpdateRepository<FinancialMovementCategory>,
    IRemoveRepository<FinancialMovementCategory>
{
}