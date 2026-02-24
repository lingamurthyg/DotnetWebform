using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Data.Configurations;
using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Infrastructure.Data.Configurations.Tests;

public class CatalogTypeConfigurationTests
{
    private readonly DbContextOptions<CatalogContext> _options;

    public CatalogTypeConfigurationTests()
    {
        _options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Configuration_ShouldBeApplied()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogType));

        Assert.NotNull(entityType);
    }

    [Fact]
    public void Configuration_ShouldSetTableName()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogType));

        Assert.Equal("CatalogType", entityType.GetTableName());
    }

    [Fact]
    public void Configuration_ShouldHavePrimaryKey()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogType));
        var primaryKey = entityType.FindPrimaryKey();

        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties[0].Name);
    }

    [Fact]
    public void Configuration_ShouldRequireType()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogType));
        var typeProperty = entityType.FindProperty("Type");

        Assert.False(typeProperty.IsNullable);
    }

    [Fact]
    public void Configuration_ShouldSetTypeMaxLength()
    {
        using var context = new CatalogContext(_options);
        var entityType = context.Model.FindEntityType(typeof(CatalogType));
        var typeProperty = entityType.FindProperty("Type");

        Assert.Equal(100, typeProperty.GetMaxLength());
    }
}
