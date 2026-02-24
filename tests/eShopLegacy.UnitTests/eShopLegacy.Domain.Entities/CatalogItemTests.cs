using System;
using Xunit;
using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Entities.Tests;

public class CatalogItemTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var catalogItem = new CatalogItem();

        Assert.NotNull(catalogItem);
        Assert.Equal(0, catalogItem.Id);
        Assert.Equal(string.Empty, catalogItem.Name);
        Assert.True(catalogItem.IsActive);
        Assert.Equal("System", catalogItem.CreatedBy);
    }

    [Fact]
    public void Name_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        var expectedName = "Test Product";

        catalogItem.Name = expectedName;

        Assert.Equal(expectedName, catalogItem.Name);
    }

    [Fact]
    public void Name_ShouldHandleEmptyString()
    {
        var catalogItem = new CatalogItem { Name = string.Empty };

        Assert.Equal(string.Empty, catalogItem.Name);
    }

    [Fact]
    public void Description_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        var expectedDescription = "Test Description";

        catalogItem.Description = expectedDescription;

        Assert.Equal(expectedDescription, catalogItem.Description);
    }

    [Fact]
    public void Description_ShouldAcceptNullValue()
    {
        var catalogItem = new CatalogItem { Description = null };

        Assert.Null(catalogItem.Description);
    }

    [Fact]
    public void Price_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        decimal expectedPrice = 99.99m;

        catalogItem.Price = expectedPrice;

        Assert.Equal(expectedPrice, catalogItem.Price);
    }

    [Fact]
    public void Price_ShouldAcceptZero()
    {
        var catalogItem = new CatalogItem { Price = 0m };

        Assert.Equal(0m, catalogItem.Price);
    }

    [Fact]
    public void Price_ShouldAcceptMaximumValue()
    {
        var catalogItem = new CatalogItem { Price = 9999999999999999.99m };

        Assert.Equal(9999999999999999.99m, catalogItem.Price);
    }

    [Fact]
    public void AvailableStock_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        int expectedStock = 100;

        catalogItem.AvailableStock = expectedStock;

        Assert.Equal(expectedStock, catalogItem.AvailableStock);
    }

    [Fact]
    public void AvailableStock_ShouldAcceptZero()
    {
        var catalogItem = new CatalogItem { AvailableStock = 0 };

        Assert.Equal(0, catalogItem.AvailableStock);
    }

    [Fact]
    public void RestockThreshold_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { RestockThreshold = 10 };

        Assert.Equal(10, catalogItem.RestockThreshold);
    }

    [Fact]
    public void MaxStockThreshold_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { MaxStockThreshold = 500 };

        Assert.Equal(500, catalogItem.MaxStockThreshold);
    }

    [Fact]
    public void OnReorder_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { OnReorder = true };

        Assert.True(catalogItem.OnReorder);
    }

    [Fact]
    public void OnReorder_ShouldDefaultToFalse()
    {
        var catalogItem = new CatalogItem();

        Assert.False(catalogItem.OnReorder);
    }

    [Fact]
    public void CatalogTypeId_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { CatalogTypeId = 1 };

        Assert.Equal(1, catalogItem.CatalogTypeId);
    }

    [Fact]
    public void CatalogBrandId_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { CatalogBrandId = 2 };

        Assert.Equal(2, catalogItem.CatalogBrandId);
    }

    [Fact]
    public void CatalogType_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };

        catalogItem.CatalogType = catalogType;

        Assert.Equal(catalogType, catalogItem.CatalogType);
    }

    [Fact]
    public void CatalogBrand_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        var catalogBrand = new CatalogBrand { Id = 1, Brand = "TestBrand" };

        catalogItem.CatalogBrand = catalogBrand;

        Assert.Equal(catalogBrand, catalogItem.CatalogBrand);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        var expectedDate = DateTime.UtcNow;

        catalogItem.CreatedDate = expectedDate;

        Assert.Equal(expectedDate, catalogItem.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldAcceptNullValue()
    {
        var catalogItem = new CatalogItem { ModifiedDate = null };

        Assert.Null(catalogItem.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem();
        var expectedDate = DateTime.UtcNow;

        catalogItem.ModifiedDate = expectedDate;

        Assert.Equal(expectedDate, catalogItem.ModifiedDate);
    }

    [Fact]
    public void IsActive_ShouldDefaultToTrue()
    {
        var catalogItem = new CatalogItem();

        Assert.True(catalogItem.IsActive);
    }

    [Fact]
    public void IsActive_ShouldSetToFalse()
    {
        var catalogItem = new CatalogItem { IsActive = false };

        Assert.False(catalogItem.IsActive);
    }

    [Fact]
    public void CreatedBy_ShouldDefaultToSystem()
    {
        var catalogItem = new CatalogItem();

        Assert.Equal("System", catalogItem.CreatedBy);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { CreatedBy = "Admin" };

        Assert.Equal("Admin", catalogItem.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldAcceptNullValue()
    {
        var catalogItem = new CatalogItem { ModifiedBy = null };

        Assert.Null(catalogItem.ModifiedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { ModifiedBy = "Admin" };

        Assert.Equal("Admin", catalogItem.ModifiedBy);
    }

    [Fact]
    public void PictureFileName_ShouldSetAndGetValue()
    {
        var catalogItem = new CatalogItem { PictureFileName = "product.jpg" };

        Assert.Equal("product.jpg", catalogItem.PictureFileName);
    }

    [Fact]
    public void PictureFileName_ShouldAcceptNull()
    {
        var catalogItem = new CatalogItem { PictureFileName = null };

        Assert.Null(catalogItem.PictureFileName);
    }

    [Fact]
    public void AllProperties_ShouldBeSettableViaObjectInitializer()
    {
        var catalogItem = new CatalogItem
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            Price = 49.99m,
            PictureFileName = "test.jpg",
            CatalogTypeId = 1,
            CatalogBrandId = 1,
            AvailableStock = 100,
            RestockThreshold = 10,
            MaxStockThreshold = 500,
            OnReorder = false,
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            CreatedBy = "System"
        };

        Assert.Equal(1, catalogItem.Id);
        Assert.Equal("Test Product", catalogItem.Name);
        Assert.Equal("Test Description", catalogItem.Description);
        Assert.Equal(49.99m, catalogItem.Price);
        Assert.Equal("test.jpg", catalogItem.PictureFileName);
        Assert.Equal(1, catalogItem.CatalogTypeId);
        Assert.Equal(1, catalogItem.CatalogBrandId);
        Assert.Equal(100, catalogItem.AvailableStock);
        Assert.Equal(10, catalogItem.RestockThreshold);
        Assert.Equal(500, catalogItem.MaxStockThreshold);
        Assert.False(catalogItem.OnReorder);
        Assert.True(catalogItem.IsActive);
        Assert.Equal("System", catalogItem.CreatedBy);
    }
}
