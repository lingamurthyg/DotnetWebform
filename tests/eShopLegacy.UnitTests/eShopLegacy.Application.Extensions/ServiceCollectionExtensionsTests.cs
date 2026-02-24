using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using eShopLegacy.Application.Extensions;
using eShopLegacy.Domain.Interfaces.Services;
using eShopLegacy.Application.Services;

namespace eShopLegacy.Application.Extensions.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddApplicationServices_ShouldReturnServiceCollection()
    {
        var services = new ServiceCollection();

        var result = services.AddApplicationServices();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IServiceCollection>(result);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterCatalogItemService()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetService<ICatalogItemService>();

        Assert.NotNull(service);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterCatalogBrandService()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetService<ICatalogBrandService>();

        Assert.NotNull(service);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterCatalogTypeService()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetService<ICatalogTypeService>();

        Assert.NotNull(service);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterAutoMapper()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();

        var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetService<AutoMapper.IMapper>();

        Assert.NotNull(mapper);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterServicesAsScoped()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();

        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(ICatalogItemService));

        Assert.NotNull(descriptor);
        Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime);
    }

    [Fact]
    public void AddApplicationServices_WithNullServices_ShouldThrowArgumentNullException()
    {
        IServiceCollection services = null;

        Assert.Throws<ArgumentNullException>(() => services.AddApplicationServices());
    }
}
