using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.DTOs;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Application.Services;

namespace eShopLegacy.Application.Services.Tests;

public class CatalogBrandServiceTests
{
    private readonly Mock<ICatalogBrandRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CatalogBrandService>> _loggerMock;
    private readonly CatalogBrandService _service;

    public CatalogBrandServiceTests()
    {
        _repositoryMock = new Mock<ICatalogBrandRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CatalogBrandService>>();
        _service = new CatalogBrandService(_repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        Assert.NotNull(_service);
    }

    [Fact]
    public void Constructor_WithNullRepository_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogBrandService(null, _mapperMock.Object, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullMapper_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogBrandService(_repositoryMock.Object, null, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogBrandService(_repositoryMock.Object, _mapperMock.Object, null));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedBrands()
    {
        var brands = new List<CatalogBrand> { new CatalogBrand { Id = 1, Brand = "Nike" } };
        var brandDtos = new List<CatalogBrandDto> { new CatalogBrandDto { Id = 1, Brand = "Nike" } };
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(brands);
        _mapperMock.Setup(m => m.Map<IEnumerable<CatalogBrandDto>>(brands)).Returns(brandDtos);

        var result = await _service.GetAllAsync();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Nike", result.First().Brand);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnMappedBrand()
    {
        var brand = new CatalogBrand { Id = 1, Brand = "Nike" };
        var brandDto = new CatalogBrandDto { Id = 1, Brand = "Nike" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(brand);
        _mapperMock.Setup(m => m.Map<CatalogBrandDto>(brand)).Returns(brandDto);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Nike", result.Brand);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((CatalogBrand)null);

        var result = await _service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedBrand()
    {
        var createDto = new CatalogBrandCreateDto { Brand = "Adidas" };
        var brand = new CatalogBrand { Brand = "Adidas" };
        var createdBrand = new CatalogBrand { Id = 1, Brand = "Adidas" };
        var brandDto = new CatalogBrandDto { Id = 1, Brand = "Adidas" };
        _mapperMock.Setup(m => m.Map<CatalogBrand>(createDto)).Returns(brand);
        _repositoryMock.Setup(r => r.AddAsync(brand, It.IsAny<CancellationToken>())).ReturnsAsync(createdBrand);
        _mapperMock.Setup(m => m.Map<CatalogBrandDto>(createdBrand)).Returns(brandDto);

        var result = await _service.CreateAsync(createDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Adidas", result.Brand);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingId_ShouldReturnUpdatedBrand()
    {
        var updateDto = new CatalogBrandUpdateDto { Brand = "UpdatedBrand" };
        var existingBrand = new CatalogBrand { Id = 1, Brand = "OriginalBrand" };
        var updatedDto = new CatalogBrandDto { Id = 1, Brand = "UpdatedBrand" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existingBrand);
        _mapperMock.Setup(m => m.Map(updateDto, existingBrand)).Returns(existingBrand);
        _mapperMock.Setup(m => m.Map<CatalogBrandDto>(existingBrand)).Returns(updatedDto);

        var result = await _service.UpdateAsync(1, updateDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ShouldThrowKeyNotFoundException()
    {
        var updateDto = new CatalogBrandUpdateDto { Brand = "UpdatedBrand" };
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((CatalogBrand)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(999, updateDto));
    }

    [Fact]
    public async Task DeleteAsync_WithExistingId_ShouldCallRepositoryDelete()
    {
        _repositoryMock.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        await _service.DeleteAsync(1);

        _repositoryMock.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingId_ShouldThrowKeyNotFoundException()
    {
        _repositoryMock.Setup(r => r.ExistsAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(999));
    }
}
