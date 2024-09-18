using FoSouzaDev.Common.Domain.Exceptions;
using FoSouzaDev.FinancialControl.Domain.Entities;
using FoSouzaDev.FinancialControl.Domain.Repositories;
using FoSouzaDev.FinancialControl.Infrastructure.Services.Interfaces;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories;

internal class FinancialMovementRepository(IUserService service) : IFinancialMovementRepository
{
    private readonly Dictionary<Guid, List<FinancialMovement>> _financialMovements = new();

    public async Task AddAsync(FinancialMovement entity)
    {
        if (_financialMovements.TryGetValue(service.GetUserId(), out var financialMovements))
            financialMovements.Add(entity);
    }

    public async Task<FinancialMovement> GetByIdAsync(Guid id)
    {
        if (_financialMovements.TryGetValue(service.GetUserId(), out var financialMovements))
            return financialMovements.SingleOrDefault(a => a.Id == id);

        return null;
    }

    public async Task<FinancialMovement> GetByIdOrThrowAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            throw new NotFoundException(id);

        return entity;
    }

    public async Task UpdateAsync(FinancialMovement entity)
    {
        if (_financialMovements.TryGetValue(service.GetUserId(), out var financialMovements))
        {
            financialMovements.Remove(entity);
            financialMovements.Add(entity);
        }
    }
}