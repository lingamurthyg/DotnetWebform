using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Entities.Tests;

public class CatalogBrandTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var catalogBrand = new CatalogBrand();

        Assert.NotNull(catalogBrand);
        Assert.Equal(0, catalogBrand.Id);
        Assert.Equal(string.Empty, catalogBrand.Brand);
        Assert.True(catalogBrand.IsActive);
        Assert.Equal("System", catalogBrand.CreatedBy);
        Assert.NotNull(catalogBrand.CatalogItems);
        Assert.Empty(catalogBrand.CatalogItems);
    }

    [Fact]
    public void Brand_ShouldSetAndGetValue()
    {
        var catalogBrand = new CatalogBrand();
        var expectedBrand = "Nike";

        catalogBrand.Brand = expectedBrand;

        Assert.Equal(expectedBrand, catalogBrand.Brand);
    }

    [Fact]
    public void Brand_ShouldHandleEmptyString()
    {
        var catalogBrand = new CatalogBrand { Brand = string.Empty };

        Assert.Equal(string.Empty, catalogBrand.Brand);
    }

    [Fact]
    public void Id_ShouldSetAndGetValue()
    {
        var catalogBrand = new CatalogBrand { Id = 1 };

        Assert.Equal(1, catalogBrand.Id);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetValue()
    {
        var catalogBrand = new CatalogBrand();
        var expectedDate = DateTime.UtcNow;

        catalogBrand.CreatedDate = expectedDate;

        Assert.Equal(expectedDate, catalogBrand.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldAcceptNullValue()
    {
        var catalogBrand = new CatalogBrand { ModifiedDate = null };

        Assert.Null(catalogBrand.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetValue()
    {
        var catalogBrand = new CatalogBrand();
        var expectedDate = DateTime.UtcNow;

        catalogBrand.ModifiedDate = expectedDate;

        Assert.Equal(expectedDate, catalogBrand.ModifiedDate);
    }

    [Fact]
    public void IsActive_ShouldDefaultToTrue()
    {
        var catalogBrand = new CatalogBrand();

        Assert.True(catalogBrand.IsActive);
    }

    [Fact]
    public void IsActive_ShouldSetToFalse()
    {
        var catalogBrand = new CatalogBrand { IsActive = false };

        Assert.False(catalogBrand.IsActive);
    }

    [Fact]
    public void CreatedBy_ShouldDefaultToSystem()
    {
        var catalogBrand = new CatalogBrand();

        Assert.Equal("System", catalogBrand.CreatedBy);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetValue()
    {
        var catalogBrand = new CatalogBrand { CreatedBy = "Admin" };

        Assert.Equal("Admin", catalogBrand.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldAcceptNullValue()
    {
        var catalogBrand = new CatalogBrand { ModifiedBy = null };

        Assert.Null(catalogBrand.ModifiedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetValue()
    {
        var catalogBrand = new CatalogBrand { ModifiedBy = "Admin" };

        Assert.Equal("Admin", catalogBrand.ModifiedBy);
    }

    [Fact]
    public void CatalogItems_ShouldBeInitializedAsEmptyList()
    {
        var catalogBrand = new CatalogBrand();

        Assert.NotNull(catalogBrand.CatalogItems);
        Assert.Empty(catalogBrand.CatalogItems);
    }

    [Fact]
    public void CatalogItems_ShouldAllowAddingItems()
    {
        var catalogBrand = new CatalogBrand();
        var catalogItem = new CatalogItem { Id = 1, Name = "Test Item" };

        catalogBrand.CatalogItems = new List<CatalogItem> { catalogItem };

        Assert.Single(catalogBrand.CatalogItems);
        Assert.Contains(catalogItem, catalogBrand.CatalogItems);
    }

    [Fact]
    public void CatalogItems_ShouldAllowMultipleItems()
    {
        var catalogBrand = new CatalogBrand();
        var item1 = new CatalogItem { Id = 1, Name = "Item 1" };
        var item2 = new CatalogItem { Id = 2, Name = "Item 2" };

        catalogBrand.CatalogItems = new List<CatalogItem> { item1, item2 };

        Assert.Equal(2, catalogBrand.CatalogItems.Count);
        Assert.Contains(item1, catalogBrand.CatalogItems);
        Assert.Contains(item2, catalogBrand.CatalogItems);
    }

    [Fact]
    public void AllProperties_ShouldBeSettableViaObjectInitializer()
    {
        var catalogBrand = new CatalogBrand
        {
            Id = 1,
            Brand = "TestBrand",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            CreatedBy = "System",
            ModifiedDate = DateTime.UtcNow,
            ModifiedBy = "Admin"
        };

        Assert.Equal(1, catalogBrand.Id);
        Assert.Equal("TestBrand", catalogBrand.Brand);
        Assert.True(catalogBrand.IsActive);
        Assert.Equal("System", catalogBrand.CreatedBy);
        Assert.Equal("Admin", catalogBrand.ModifiedBy);
        Assert.NotNull(catalogBrand.CreatedDate);
        Assert.NotNull(catalogBrand.ModifiedDate);
    }
}
