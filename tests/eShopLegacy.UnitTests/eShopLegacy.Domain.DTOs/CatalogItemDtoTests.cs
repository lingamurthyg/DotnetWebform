using System;
using Xunit;
using eShopLegacy.Domain.DTOs;

namespace eShopLegacy.Domain.DTOs.Tests;

public class CatalogItemDtoTests
{
    [Fact]
    public void CatalogItemDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogItemDto();

        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(0m, dto.Price);
    }

    [Fact]
    public void CatalogItemDto_AllProperties_ShouldSetAndGetValues()
    {
        var dto = new CatalogItemDto
        {
            Id = 1,
            Name = "TestProduct",
            Description = "Test Description",
            Price = 99.99m,
            PictureFileName = "test.jpg",
            CatalogTypeId = 1,
            CatalogTypeName = "Electronics",
            CatalogBrandId = 2,
            CatalogBrandName = "TestBrand",
            AvailableStock = 100,
            RestockThreshold = 10,
            MaxStockThreshold = 500,
            OnReorder = true
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("TestProduct", dto.Name);
        Assert.Equal("Test Description", dto.Description);
        Assert.Equal(99.99m, dto.Price);
        Assert.Equal("test.jpg", dto.PictureFileName);
        Assert.Equal(1, dto.CatalogTypeId);
        Assert.Equal("Electronics", dto.CatalogTypeName);
        Assert.Equal(2, dto.CatalogBrandId);
        Assert.Equal("TestBrand", dto.CatalogBrandName);
        Assert.Equal(100, dto.AvailableStock);
        Assert.Equal(10, dto.RestockThreshold);
        Assert.Equal(500, dto.MaxStockThreshold);
        Assert.True(dto.OnReorder);
    }

    [Fact]
    public void CatalogItemCreateDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogItemCreateDto();

        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(0m, dto.Price);
    }

    [Fact]
    public void CatalogItemCreateDto_AllProperties_ShouldSetAndGetValues()
    {
        var dto = new CatalogItemCreateDto
        {
            Name = "NewProduct",
            Description = "New Description",
            Price = 49.99m,
            PictureFileName = "new.jpg",
            CatalogTypeId = 1,
            CatalogBrandId = 2,
            AvailableStock = 50,
            RestockThreshold = 5,
            MaxStockThreshold = 200,
            OnReorder = false
        };

        Assert.Equal("NewProduct", dto.Name);
        Assert.Equal("New Description", dto.Description);
        Assert.Equal(49.99m, dto.Price);
        Assert.Equal("new.jpg", dto.PictureFileName);
        Assert.Equal(1, dto.CatalogTypeId);
        Assert.Equal(2, dto.CatalogBrandId);
        Assert.Equal(50, dto.AvailableStock);
        Assert.Equal(5, dto.RestockThreshold);
        Assert.Equal(200, dto.MaxStockThreshold);
        Assert.False(dto.OnReorder);
    }

    [Fact]
    public void CatalogItemUpdateDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogItemUpdateDto();

        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(0m, dto.Price);
    }

    [Fact]
    public void CatalogItemUpdateDto_AllProperties_ShouldSetAndGetValues()
    {
        var dto = new CatalogItemUpdateDto
        {
            Name = "UpdatedProduct",
            Description = "Updated Description",
            Price = 79.99m,
            PictureFileName = "updated.jpg",
            CatalogTypeId = 3,
            CatalogBrandId = 4,
            AvailableStock = 75,
            RestockThreshold = 8,
            MaxStockThreshold = 300,
            OnReorder = true
        };

        Assert.Equal("UpdatedProduct", dto.Name);
        Assert.Equal("Updated Description", dto.Description);
        Assert.Equal(79.99m, dto.Price);
        Assert.Equal("updated.jpg", dto.PictureFileName);
        Assert.Equal(3, dto.CatalogTypeId);
        Assert.Equal(4, dto.CatalogBrandId);
        Assert.Equal(75, dto.AvailableStock);
        Assert.Equal(8, dto.RestockThreshold);
        Assert.Equal(300, dto.MaxStockThreshold);
        Assert.True(dto.OnReorder);
    }

    [Fact]
    public void CatalogItemDto_NullableProperties_ShouldAcceptNull()
    {
        var dto = new CatalogItemDto
        {
            Description = null,
            PictureFileName = null,
            CatalogTypeName = null,
            CatalogBrandName = null
        };

        Assert.Null(dto.Description);
        Assert.Null(dto.PictureFileName);
        Assert.Null(dto.CatalogTypeName);
        Assert.Null(dto.CatalogBrandName);
    }
}
