using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Data.Configurations;
using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Infrastructure.Data.Configurations.Tests;

public class CatalogBrandConfigurationTests
{
    private readonly DbContextOptions<CatalogContext> _options;

    public CatalogBrandConfigurationTests()
    {
        _options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Configuration_ShouldBeApplied()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogBrand));

        Assert.NotNull(entityType);
    }

    [Fact]
    public void Configuration_ShouldSetTableName()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogBrand));

        Assert.Equal("CatalogBrand", entityType.GetTableName());
    }

    [Fact]
    public void Configuration_ShouldHavePrimaryKey()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogBrand));
        var primaryKey = entityType.FindPrimaryKey();

        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties[0].Name);
    }

    [Fact]
    public void Configuration_ShouldRequireBrand()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogBrand));
        var brandProperty = entityType.FindProperty("Brand");

        Assert.False(brandProperty.IsNullable);
    }

    [Fact]
    public void Configuration_ShouldSetBrandMaxLength()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogBrand));
        var brandProperty = entityType.FindProperty("Brand");

        Assert.Equal(100, brandProperty.GetMaxLength());
    }
}
