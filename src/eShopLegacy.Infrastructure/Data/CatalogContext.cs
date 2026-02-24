using eShopLegacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopLegacy.Infrastructure.Data;

/// <summary>
/// Database context for the catalog database
/// </summary>
public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
    public DbSet<CatalogType> CatalogTypes => Set<CatalogType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set default schema to public for PostgreSQL
        modelBuilder.HasDefaultSchema("public");

        // Enable PostgreSQL extensions if needed
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
    }
}
