using FoSouzaDev.FinancialControl.Infrastructure.DataEntities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FoSouzaDev.FinancialControl.Infrastructure.Repositories.MongoDatabase;

internal class MongoDbContext(DbContextOptions<MongoDbContext> options) : DbContext(options)
{
    public DbSet<FinancialMovementCategoryDataEntity> FinancialMovementCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<FinancialMovementCategoryDataEntity>().ToCollection("financialMovementCategories");
    }
}