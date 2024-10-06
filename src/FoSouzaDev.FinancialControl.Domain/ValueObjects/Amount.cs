using FoSouzaDev.Common.Domain.Exceptions;

namespace FoSouzaDev.FinancialControl.Domain.ValueObjects;

public sealed record Amount
{
    public decimal Value { get; private set; }

    public Amount(decimal value)
    {
        if (value <= 0)
            throw new ValidateException($"{nameof(Amount)} cannot be less than or equal to zero.");

        Value = value;
    }
}