using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopLegacy.Infrastructure.Extensions;

/// <summary>
/// Extension methods for registering infrastructure services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogConnection")
            ?? "Host=localhost;Port=5432;Database=CatalogDb;Username=postgres;Password=postgres";

        services.AddDbContext<CatalogContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
                npgsqlOptions.MigrationsHistoryTable("__ef_migrations_history", "public");
            })
            .UseSnakeCaseNamingConvention();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        });

        services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
        services.AddScoped<ICatalogBrandRepository, CatalogBrandRepository>();
        services.AddScoped<ICatalogTypeRepository, CatalogTypeRepository>();

        return services;
    }
}
