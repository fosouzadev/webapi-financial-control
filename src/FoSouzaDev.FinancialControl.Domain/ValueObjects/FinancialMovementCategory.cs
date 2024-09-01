namespace FoSouzaDev.FinancialControl.Domain.ValueObjects
{
    public sealed class FinancialMovementCategory
    {
        // como persistir um ValueObject
        public string? Id { get; private set; }
        public string? Name { get; private set; }

        public FinancialMovementCategory()
        {
            
        }
    }
}