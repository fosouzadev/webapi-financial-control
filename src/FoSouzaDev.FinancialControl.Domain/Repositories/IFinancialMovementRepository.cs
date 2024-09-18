using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories.Generics;

namespace FoSouzaDev.FinancialControl.Domain.Repositories;

public interface IFinancialMovementRepository :
    IAddRepository<FinancialMovement>,
    IGetRepository<FinancialMovement>,
    IUpdateRepository<FinancialMovement>
{
}