namespace FoSouzaDev.FinancialControl.Domain.Factories;

internal abstract class FactoryBase
{
    protected void ThrowIfIsNotValidValue<TEnum>(byte type) where TEnum : Enum
    {
        if (Enum.IsDefined(typeof(TEnum), type) == false)
            throw new ArgumentException($"Invalid value for enum {typeof(TEnum).Name}: {type}");
    }
}