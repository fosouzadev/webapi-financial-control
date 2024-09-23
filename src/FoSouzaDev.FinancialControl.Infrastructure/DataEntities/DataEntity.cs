namespace FoSouzaDev.FinancialControl.Infrastructure.DataEntities;

internal abstract class DataEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDateTime { get; set; }
    public Guid UserId { get; set; }
}