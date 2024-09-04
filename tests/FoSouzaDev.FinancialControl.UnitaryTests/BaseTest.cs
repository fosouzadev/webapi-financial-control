using AutoFixture;

namespace FoSouzaDev.FinancialControl.UnitaryTests;

public abstract class BaseTest
{
    protected Fixture Fixture { get; private set; }

    protected BaseTest()
    {
        Fixture = new();
    }
}