using System;
using Xunit;
using eShopLegacy.Domain.DTOs;

namespace eShopLegacy.Domain.DTOs.Tests;

public class CatalogTypeDtoTests
{
    [Fact]
    public void CatalogTypeDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogTypeDto();

        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Type);
    }

    [Fact]
    public void CatalogTypeDto_AllProperties_ShouldSetAndGetValues()
    {
        var dto = new CatalogTypeDto
        {
            Id = 1,
            Type = "Electronics"
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Electronics", dto.Type);
    }

    [Fact]
    public void CatalogTypeCreateDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogTypeCreateDto();

        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Type);
    }

    [Fact]
    public void CatalogTypeCreateDto_Type_ShouldSetAndGetValue()
    {
        var dto = new CatalogTypeCreateDto { Type = "Books" };

        Assert.Equal("Books", dto.Type);
    }

    [Fact]
    public void CatalogTypeUpdateDto_Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new CatalogTypeUpdateDto();

        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Type);
    }

    [Fact]
    public void CatalogTypeUpdateDto_Type_ShouldSetAndGetValue()
    {
        var dto = new CatalogTypeUpdateDto { Type = "Clothing" };

        Assert.Equal("Clothing", dto.Type);
    }

    [Fact]
    public void CatalogTypeDto_Type_ShouldHandleEmptyString()
    {
        var dto = new CatalogTypeDto { Type = string.Empty };

        Assert.Equal(string.Empty, dto.Type);
    }
}
