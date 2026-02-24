using System;
using Xunit;
using eShopLegacy.Domain.DTOs;

namespace eShopLegacy.Domain.DTOs.Tests;

public class CatalogBrandDtoTests
{
    [Fact]
    public void CatalogBrandDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogBrandDto();

        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Brand);
    }

    [Fact]
    public void CatalogBrandDto_AllProperties_ShouldSetAndGetValues()
    {
        var dto = new CatalogBrandDto
        {
            Id = 1,
            Brand = "Nike"
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Nike", dto.Brand);
    }

    [Fact]
    public void CatalogBrandCreateDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogBrandCreateDto();

        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Brand);
    }

    [Fact]
    public void CatalogBrandCreateDto_Brand_ShouldSetAndGetValue()
    {
        var dto = new CatalogBrandCreateDto { Brand = "Adidas" };

        Assert.Equal("Adidas", dto.Brand);
    }

    [Fact]
    public void CatalogBrandUpdateDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogBrandUpdateDto();

        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Brand);
    }

    [Fact]
    public void CatalogBrandUpdateDto_Brand_ShouldSetAndGetValue()
    {
        var dto = new CatalogBrandUpdateDto { Brand = "Puma" };

        Assert.Equal("Puma", dto.Brand);
    }

    [Fact]
    public void CatalogBrandDto_Brand_ShouldHandleEmptyString()
    {
        var dto = new CatalogBrandDto { Brand = string.Empty };

        Assert.Equal(string.Empty, dto.Brand);
    }
}
