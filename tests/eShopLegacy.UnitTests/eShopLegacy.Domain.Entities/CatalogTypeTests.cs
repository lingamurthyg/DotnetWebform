using System;
using System.Collections.Generic;
using Xunit;
using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Entities.Tests;

public class CatalogTypeTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var catalogType = new CatalogType();

        Assert.NotNull(catalogType);
        Assert.Equal(0, catalogType.Id);
        Assert.Equal(string.Empty, catalogType.Type);
        Assert.True(catalogType.IsActive);
        Assert.Equal("System", catalogType.CreatedBy);
        Assert.NotNull(catalogType.CatalogItems);
        Assert.Empty(catalogType.CatalogItems);
    }

    [Fact]
    public void Type_ShouldSetAndGetValue()
    {
        var catalogType = new CatalogType();
        var expectedType = "Electronics";

        catalogType.Type = expectedType;

        Assert.Equal(expectedType, catalogType.Type);
    }

    [Fact]
    public void Type_ShouldHandleEmptyString()
    {
        var catalogType = new CatalogType { Type = string.Empty };

        Assert.Equal(string.Empty, catalogType.Type);
    }

    [Fact]
    public void Id_ShouldSetAndGetValue()
    {
        var catalogType = new CatalogType { Id = 1 };

        Assert.Equal(1, catalogType.Id);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetValue()
    {
        var catalogType = new CatalogType();
        var expectedDate = DateTime.UtcNow;

        catalogType.CreatedDate = expectedDate;

        Assert.Equal(expectedDate, catalogType.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldAcceptNullValue()
    {
        var catalogType = new CatalogType { ModifiedDate = null };

        Assert.Null(catalogType.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetValue()
    {
        var catalogType = new CatalogType();
        var expectedDate = DateTime.UtcNow;

        catalogType.ModifiedDate = expectedDate;

        Assert.Equal(expectedDate, catalogType.ModifiedDate);
    }

    [Fact]
    public void IsActive_ShouldDefaultToTrue()
    {
        var catalogType = new CatalogType();

        Assert.True(catalogType.IsActive);
    }

    [Fact]
    public void IsActive_ShouldSetToFalse()
    {
        var catalogType = new CatalogType { IsActive = false };

        Assert.False(catalogType.IsActive);
    }

    [Fact]
    public void CreatedBy_ShouldDefaultToSystem()
    {
        var catalogType = new CatalogType();

        Assert.Equal("System", catalogType.CreatedBy);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetValue()
    {
        var catalogType = new CatalogType { CreatedBy = "Admin" };

        Assert.Equal("Admin", catalogType.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldAcceptNullValue()
    {
        var catalogType = new CatalogType { ModifiedBy = null };

        Assert.Null(catalogType.ModifiedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetValue()
    {
        var catalogType = new CatalogType { ModifiedBy = "Admin" };

        Assert.Equal("Admin", catalogType.ModifiedBy);
    }

    [Fact]
    public void CatalogItems_ShouldBeInitializedAsEmptyList()
    {
        var catalogType = new CatalogType();

        Assert.NotNull(catalogType.CatalogItems);
        Assert.Empty(catalogType.CatalogItems);
    }

    [Fact]
    public void CatalogItems_ShouldAllowAddingItems()
    {
        var catalogType = new CatalogType();
        var catalogItem = new CatalogItem { Id = 1, Name = "Test Item" };

        catalogType.CatalogItems = new List<CatalogItem> { catalogItem };

        Assert.Single(catalogType.CatalogItems);
        Assert.Contains(catalogItem, catalogType.CatalogItems);
    }

    [Fact]
    public void CatalogItems_ShouldAllowMultipleItems()
    {
        var catalogType = new CatalogType();
        var item1 = new CatalogItem { Id = 1, Name = "Item 1" };
        var item2 = new CatalogItem { Id = 2, Name = "Item 2" };

        catalogType.CatalogItems = new List<CatalogItem> { item1, item2 };

        Assert.Equal(2, catalogType.CatalogItems.Count);
        Assert.Contains(item1, catalogType.CatalogItems);
        Assert.Contains(item2, catalogType.CatalogItems);
    }

    [Fact]
    public void AllProperties_ShouldBeSettableViaObjectInitializer()
    {
        var catalogType = new CatalogType
        {
            Id = 1,
            Type = "Electronics",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            CreatedBy = "System",
            ModifiedDate = DateTime.UtcNow,
            ModifiedBy = "Admin"
        };

        Assert.Equal(1, catalogType.Id);
        Assert.Equal("Electronics", catalogType.Type);
        Assert.True(catalogType.IsActive);
        Assert.Equal("System", catalogType.CreatedBy);
        Assert.Equal("Admin", catalogType.ModifiedBy);
        Assert.NotNull(catalogType.CreatedDate);
        Assert.NotNull(catalogType.ModifiedDate);
    }
}
