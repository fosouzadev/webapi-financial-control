using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces.Base;

namespace FoSouzaDev.FinancialControl.Application.Services.Interfaces;

public interface IFinancialMovementAppService : IPartialActionsAppService<AddFinancialMovementDto, GetFinancialMovementDto, UpdateFinancialMovementDto>
{
}