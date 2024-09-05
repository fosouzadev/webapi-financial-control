using FoSouzaDev.FinancialControl.Domain.Exceptions;

namespace FoSouzaDev.FinancialControl.Domain.Entities;

public sealed class FinancialMovementCategory
{
    private string _name;

    public Guid Id { get; private init; }
    public string Name // transformar em VO
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidateException($"{nameof(Name)} cannot be null or empty.");

            _name = value;
        }
    }

    public FinancialMovementCategory(string name, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
    }
}