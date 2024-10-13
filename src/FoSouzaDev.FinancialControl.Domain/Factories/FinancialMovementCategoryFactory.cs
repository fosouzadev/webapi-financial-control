using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Factories.Interfaces;
using FoSouzaDev.FinancialControl.Domain.ValueObjects;

namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal sealed class FinancialMovementCategoryFactory : IFinancialMovementCategoryFactory
{
    public FinancialMovementCategory CreateEntity(string name) =>
        RebuildEntity(name, DateTimeOffset.UtcNow, Guid.NewGuid());

    public FinancialMovementCategory RebuildEntity(string name, DateTimeOffset creationDateTime, Guid id) =>
        new(new Name(name), creationDateTime, id);
}