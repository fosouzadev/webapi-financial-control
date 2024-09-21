namespace FoSouzaDev.FinancialControl.Infrastructure.DataEntities;

internal class FinancialMovementCategoryDataEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDateTime { get; set; }
    public Guid UserId { get; set; }
}