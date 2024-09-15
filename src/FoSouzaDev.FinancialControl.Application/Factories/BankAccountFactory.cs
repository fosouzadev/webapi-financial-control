//using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
//using FoSouzaDev.FinancialControl.Application.Factories.Interfaces;
//using FoSouzaDev.FinancialControl.Domain.Entities;
//using FoSouzaDev.FinancialControl.Domain.ValueObjects;

//namespace FoSouzaDev.FinancialControl.Application.Factories;

//internal sealed class BankAccountFactory : IBankAccountFactory
//{
//    public BankAccountDto DomainEntityToDto(BankAccount entity) =>
//        new()
//        {
//            Id = entity.Id,
//            Name = entity.Name.Value,
//            Description = entity.Description,
//            IsActive = entity.IsActive,
//            Balance = entity.Balance,
//            Type = entity.Type,
//            CreationDateTime = entity.CreationDateTime
//        };

//    public UpdateBankAccountDto DomainEntityToUpdateDto(BankAccount entity) =>
//        new()
//        {
//            Name = entity.Name.Value,
//            Description = entity.Description,
//            IsActive = entity.IsActive
//        };

//    public BankAccount AddDtoToDomainEntity(AddBankAccountDto dto) =>
//        new(new Name(dto.Name), dto.Description, isActive: true, dto.Type);
//}