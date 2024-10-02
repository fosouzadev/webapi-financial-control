using FoSouzaDev.FinancialControl.Application.DataTransferObjects;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IFinancialMovementAppService : IPartialActionsAppService<AddFinancialMovementDto, GetFinancialMovementDto, UpdateFinancialMovementDto>
{
}