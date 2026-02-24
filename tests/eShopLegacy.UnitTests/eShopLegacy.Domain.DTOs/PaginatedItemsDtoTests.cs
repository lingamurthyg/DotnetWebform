using System;
using System.Collections.Generic;
using Xunit;
using eShopLegacy.Domain.DTOs;

namespace eShopLegacy.Domain.DTOs.Tests;

public class PaginatedItemsDtoTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var dto = new PaginatedItemsDto<string>();

        Assert.NotNull(dto);
        Assert.Equal(0, dto.PageIndex);
        Assert.Equal(0, dto.PageSize);
        Assert.Equal(0, dto.TotalCount);
        Assert.Equal(0, dto.TotalPages);
        Assert.NotNull(dto.Data);
        Assert.Empty(dto.Data);
    }

    [Fact]
    public void PageIndex_ShouldSetAndGetValue()
    {
        var dto = new PaginatedItemsDto<string> { PageIndex = 2 };

        Assert.Equal(2, dto.PageIndex);
    }

    [Fact]
    public void PageSize_ShouldSetAndGetValue()
    {
        var dto = new PaginatedItemsDto<string> { PageSize = 10 };

        Assert.Equal(10, dto.PageSize);
    }

    [Fact]
    public void TotalCount_ShouldSetAndGetValue()
    {
        var dto = new PaginatedItemsDto<string> { TotalCount = 100 };

        Assert.Equal(100, dto.TotalCount);
    }

    [Fact]
    public void TotalPages_ShouldSetAndGetValue()
    {
        var dto = new PaginatedItemsDto<string> { TotalPages = 10 };

        Assert.Equal(10, dto.TotalPages);
    }

    [Fact]
    public void Data_ShouldSetAndGetValue()
    {
        var data = new List<string> { "Item1", "Item2" };
        var dto = new PaginatedItemsDto<string> { Data = data };

        Assert.Equal(data, dto.Data);
        Assert.Equal(2, dto.Data.Count());
    }

    [Fact]
    public void HasPreviousPage_WhenPageIndexIsZero_ShouldReturnFalse()
    {
        var dto = new PaginatedItemsDto<string> { PageIndex = 0 };

        Assert.False(dto.HasPreviousPage);
    }

    [Fact]
    public void HasPreviousPage_WhenPageIndexIsGreaterThanZero_ShouldReturnTrue()
    {
        var dto = new PaginatedItemsDto<string> { PageIndex = 1 };

        Assert.True(dto.HasPreviousPage);
    }

    [Fact]
    public void HasNextPage_WhenPageIndexIsLastPage_ShouldReturnFalse()
    {
        var dto = new PaginatedItemsDto<string>
        {
            PageIndex = 9,
            TotalPages = 10
        };

        Assert.False(dto.HasNextPage);
    }

    [Fact]
    public void HasNextPage_WhenPageIndexIsNotLastPage_ShouldReturnTrue()
    {
        var dto = new PaginatedItemsDto<string>
        {
            PageIndex = 5,
            TotalPages = 10
        };

        Assert.True(dto.HasNextPage);
    }

    [Fact]
    public void GenericType_ShouldWorkWithDifferentTypes()
    {
        var intDto = new PaginatedItemsDto<int> { Data = new List<int> { 1, 2, 3 } };
        var stringDto = new PaginatedItemsDto<string> { Data = new List<string> { "a", "b" } };

        Assert.Equal(3, intDto.Data.Count());
        Assert.Equal(2, stringDto.Data.Count());
    }

    [Fact]
    public void AllProperties_ShouldBeSettableViaObjectInitializer()
    {
        var dto = new PaginatedItemsDto<string>
        {
            PageIndex = 2,
            PageSize = 10,
            TotalCount = 100,
            TotalPages = 10,
            Data = new List<string> { "Item1", "Item2" }
        };

        Assert.Equal(2, dto.PageIndex);
        Assert.Equal(10, dto.PageSize);
        Assert.Equal(100, dto.TotalCount);
        Assert.Equal(10, dto.TotalPages);
        Assert.Equal(2, dto.Data.Count());
    }
}
