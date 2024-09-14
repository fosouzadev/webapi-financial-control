namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects
{
    public sealed record UpdateFinancialMovementDto
    {
        public required string Name { get; set; }
        public required decimal Amount { get; set; }
        public required Guid CategoryId { get; set; }
    }
}