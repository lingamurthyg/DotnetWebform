using System;
using Xunit;
using AutoMapper;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.DTOs;
using eShopLegacy.Application.Mappings;

namespace eShopLegacy.Application.Mappings.Tests;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void MappingProfile_Configuration_ShouldBeValid()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void Map_CatalogItemToDto_ShouldMapCorrectly()
    {
        var item = new CatalogItem
        {
            Id = 1,
            Name = "TestItem",
            Price = 99.99m,
            CatalogType = new CatalogType { Type = "Electronics" },
            CatalogBrand = new CatalogBrand { Brand = "TestBrand" }
        };

        var dto = _mapper.Map<CatalogItemDto>(item);

        Assert.Equal(item.Id, dto.Id);
        Assert.Equal(item.Name, dto.Name);
        Assert.Equal(item.Price, dto.Price);
        Assert.Equal("Electronics", dto.CatalogTypeName);
        Assert.Equal("TestBrand", dto.CatalogBrandName);
    }

    [Fact]
    public void Map_CatalogItemCreateDtoToEntity_ShouldMapCorrectly()
    {
        var createDto = new CatalogItemCreateDto
        {
            Name = "NewItem",
            Price = 49.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };

        var entity = _mapper.Map<CatalogItem>(createDto);

        Assert.Equal(createDto.Name, entity.Name);
        Assert.Equal(createDto.Price, entity.Price);
        Assert.Equal(createDto.CatalogTypeId, entity.CatalogTypeId);
        Assert.Equal(createDto.CatalogBrandId, entity.CatalogBrandId);
    }

    [Fact]
    public void Map_CatalogItemUpdateDtoToEntity_ShouldMapCorrectly()
    {
        var updateDto = new CatalogItemUpdateDto
        {
            Name = "UpdatedItem",
            Price = 79.99m
        };

        var entity = _mapper.Map<CatalogItem>(updateDto);

        Assert.Equal(updateDto.Name, entity.Name);
        Assert.Equal(updateDto.Price, entity.Price);
    }

    [Fact]
    public void Map_CatalogBrandToDto_ShouldMapCorrectly()
    {
        var brand = new CatalogBrand { Id = 1, Brand = "Nike" };

        var dto = _mapper.Map<CatalogBrandDto>(brand);

        Assert.Equal(brand.Id, dto.Id);
        Assert.Equal(brand.Brand, dto.Brand);
    }

    [Fact]
    public void Map_CatalogBrandCreateDtoToEntity_ShouldMapCorrectly()
    {
        var createDto = new CatalogBrandCreateDto { Brand = "Adidas" };

        var entity = _mapper.Map<CatalogBrand>(createDto);

        Assert.Equal(createDto.Brand, entity.Brand);
    }

    [Fact]
    public void Map_CatalogBrandUpdateDtoToEntity_ShouldMapCorrectly()
    {
        var updateDto = new CatalogBrandUpdateDto { Brand = "Puma" };

        var entity = _mapper.Map<CatalogBrand>(updateDto);

        Assert.Equal(updateDto.Brand, entity.Brand);
    }

    [Fact]
    public void Map_CatalogTypeToDto_ShouldMapCorrectly()
    {
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };

        var dto = _mapper.Map<CatalogTypeDto>(catalogType);

        Assert.Equal(catalogType.Id, dto.Id);
        Assert.Equal(catalogType.Type, dto.Type);
    }

    [Fact]
    public void Map_CatalogTypeCreateDtoToEntity_ShouldMapCorrectly()
    {
        var createDto = new CatalogTypeCreateDto { Type = "Books" };

        var entity = _mapper.Map<CatalogType>(createDto);

        Assert.Equal(createDto.Type, entity.Type);
    }

    [Fact]
    public void Map_CatalogTypeUpdateDtoToEntity_ShouldMapCorrectly()
    {
        var updateDto = new CatalogTypeUpdateDto { Type = "Clothing" };

        var entity = _mapper.Map<CatalogType>(updateDto);

        Assert.Equal(updateDto.Type, entity.Type);
    }
}
