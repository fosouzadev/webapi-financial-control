namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record UpdateBankAccountDto
{
    public required string Name { get; set; }
    public string Description { get; set; }
    public required bool IsActive { get; set; }
}