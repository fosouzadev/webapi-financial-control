//using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
//using FoSouzaDev.FinancialControl.Application.Factories.Interfaces;
//using FoSouzaDev.FinancialControl.Domain.Entities;
//using FoSouzaDev.FinancialControl.Domain.ValueObjects;

//namespace FoSouzaDev.FinancialControl.Application.Factories;

//internal sealed class FinancialMovementCategoryFactory : IFinancialMovementCategoryFactory
//{
//    public FinancialMovementCategoryDto DomainEntityToDto(FinancialMovementCategory entity) =>
//        new()
//        {
//            Id = entity.Id,
//            Name = entity.Name.Value
//        };

//    public UpdateFinancialMovementCategoryDto DomainEntityToUpdateDto(FinancialMovementCategory entity) =>
//        new()
//        {
//            Name = entity.Name.Value
//        };

//    public FinancialMovementCategory AddDtoToDomainEntity(AddFinancialMovementCategoryDto dto) =>
//        new(new Name(dto.Name));
//}