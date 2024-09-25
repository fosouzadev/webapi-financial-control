namespace FoSouzaDev.FinancialControl.Infrastructure.DataEntities;

internal sealed class BankAccountDataEntity : DataEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public decimal Balance { get; set; }
    public byte Type { get; set; }
}