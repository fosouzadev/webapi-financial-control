using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IFinancialMovementCategoryFactory
{
    FinancialMovementCategory CreateEntity(string name);
    FinancialMovementCategory RebuildEntity(string name, DateTimeOffset creationDateTime, Guid id);
}