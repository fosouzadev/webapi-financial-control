namespace FoSouzaDev.FinancialControl.Infrastructure.DataEntities;

internal sealed class FinancialMovementDataEntity : DataEntity
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public byte Type { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BankAccountId { get; set; }
}