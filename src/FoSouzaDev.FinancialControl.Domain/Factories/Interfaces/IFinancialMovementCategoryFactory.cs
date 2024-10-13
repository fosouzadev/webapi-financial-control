using FoSouzaDev.FinancialControl.Domain.DataTransferObjects;
using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IFinancialMovementCategoryFactory
    : IDomainFactory<FinancialMovementCategory, FinancialMovementCategoryCreateDto, FinancialMovementCategoryRebuildDto>
{
}