//using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
//using FoSouzaDev.FinancialControl.Application.Factories.Interfaces;
//using FoSouzaDev.FinancialControl.Domain.Entities;

//namespace FoSouzaDev.FinancialControl.Application.Factories;

//internal sealed class FinancialMovementFactory : IFinancialMovementFactory
//{
//    public FinancialMovementDto DomainEntityToDto(FinancialMovement entity) =>
//        new()
//        {
//            Id = entity.Id,
//            Name = entity.Name.Value,
//            Amount = entity.Amount.Value,
//            Type = entity.Type,
//            CategoryId = entity.Category.Id,
//            CreationDateTime = entity.CreationDateTime
//        };

//    public UpdateFinancialMovementDto DomainEntityToUpdateDto(FinancialMovement entity) =>
//        new()
//        {
//            Name = entity.Name.Value,
//            Amount = entity.Amount.Value,
//            CategoryId = entity.Category.Id
//        };
//}