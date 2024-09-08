using FoSouzaDev.Common.Domain.Exceptions;

namespace FoSouzaDev.FinancialControl.Domain.ValueObjects
{
    public sealed record Name
    {
        public string Value { get; private set; }

        public Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidateException($"{nameof(Name)} cannot be null or empty.");

            Value = name;
        }
    }
}