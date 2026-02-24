using eShopLegacy.Application.Services;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eShopLegacy.Application.Extensions;

/// <summary>
/// Extension methods for registering application services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<ICatalogItemService, CatalogItemService>();
        services.AddScoped<ICatalogBrandService, CatalogBrandService>();
        services.AddScoped<ICatalogTypeService, CatalogTypeService>();

        return services;
    }
}
