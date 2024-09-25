using FoSouzaDev.FinancialControl.Domain.Entities;

namespace FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;

public interface IFinancialMovementFactory
{
    Task<FinancialMovement> CreateEntityAsync(string name, decimal amount, byte type, Guid categoryId, Guid bankAccountId);
    Task<FinancialMovement> RebuildEntityAsync(string name, decimal amount, byte type, Guid categoryId, Guid bankAccountId, DateTimeOffset creationDateTime, Guid id);
}