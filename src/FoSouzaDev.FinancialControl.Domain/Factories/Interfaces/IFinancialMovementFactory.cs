using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Enums;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IFinancialMovementFactory
{
    FinancialMovement CreateEntityAsync(string name, decimal amount, FinancialMovementType type, FinancialMovementCategory category);
    FinancialMovement RebuildEntity(string name, decimal amount, FinancialMovementType type, FinancialMovementCategory category, DateTimeOffset creationDateTime, Guid id);
}