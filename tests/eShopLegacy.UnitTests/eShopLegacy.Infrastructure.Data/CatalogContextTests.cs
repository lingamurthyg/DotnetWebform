using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Infrastructure.Data.Tests;

public class CatalogContextTests
{
    private readonly DbContextOptions<CatalogContext> _options;

    public CatalogContextTests()
    {
        _options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithValidOptions_ShouldCreateInstance()
    {
        using var context = new CatalogContext(_options);

        Assert.NotNull(context);
    }

    [Fact]
    public void CatalogItems_ShouldBeAccessible()
    {
        using var context = new CatalogContext(_options);

        Assert.NotNull(context.CatalogItems);
    }

    [Fact]
    public void CatalogBrands_ShouldBeAccessible()
    {
        using var context = new CatalogContext(_options);

        Assert.NotNull(context.CatalogBrands);
    }

    [Fact]
    public void CatalogTypes_ShouldBeAccessible()
    {
        using var context = new CatalogContext(_options);

        Assert.NotNull(context.CatalogTypes);
    }

    [Fact]
    public void CatalogContext_ShouldAllowAddingCatalogItem()
    {
        using var context = new CatalogContext(_options);
        var item = new CatalogItem
        {
            Name = "TestItem",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };

        context.CatalogItems.Add(item);
        context.SaveChanges();

        Assert.Equal(1, context.CatalogItems.Count());
    }

    [Fact]
    public void CatalogContext_ShouldAllowAddingCatalogBrand()
    {
        using var context = new CatalogContext(_options);
        var brand = new CatalogBrand { Brand = "TestBrand" };

        context.CatalogBrands.Add(brand);
        context.SaveChanges();

        Assert.Equal(1, context.CatalogBrands.Count());
    }

    [Fact]
    public void CatalogContext_ShouldAllowAddingCatalogType()
    {
        using var context = new CatalogContext(_options);
        var catalogType = new CatalogType { Type = "Electronics" };

        context.CatalogTypes.Add(catalogType);
        context.SaveChanges();

        Assert.Equal(1, context.CatalogTypes.Count());
    }
}
