namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories.MongoDatabase;

internal sealed class MongoDbSettings
{
    public string ConnectionUri { get; init; }
    public string DatabaseName { get; init; }
}