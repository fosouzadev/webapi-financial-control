﻿using FoSouzaDev.FinancialControl.Application.Enums;

namespace FoSouzaDev.FinancialControl.Application.DataTransferObjects;

public sealed record AddBankAccountDto
{
    public required string Name { get; init; }
    public string Description { get; init; }
    public required BankAccountTypeApp Type { get; init; }
}