using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Data.Configurations;
using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Infrastructure.Data.Configurations.Tests;

public class CatalogItemConfigurationTests
{
    private readonly DbContextOptions<CatalogContext> _options;

    public CatalogItemConfigurationTests()
    {
        _options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Configuration_ShouldBeApplied()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogItem));

        Assert.NotNull(entityType);
    }

    [Fact]
    public void Configuration_ShouldSetTableName()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogItem));

        Assert.Equal("Catalog", entityType.GetTableName());
    }

    [Fact]
    public void Configuration_ShouldHavePrimaryKey()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogItem));
        var primaryKey = entityType.FindPrimaryKey();

        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties[0].Name);
    }

    [Fact]
    public void Configuration_ShouldRequireName()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogItem));
        var nameProperty = entityType.FindProperty("Name");

        Assert.False(nameProperty.IsNullable);
    }

    [Fact]
    public void Configuration_ShouldSetNameMaxLength()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogItem));
        var nameProperty = entityType.FindProperty("Name");

        Assert.Equal(100, nameProperty.GetMaxLength());
    }
}
