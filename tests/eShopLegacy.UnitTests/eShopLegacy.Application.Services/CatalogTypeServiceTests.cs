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

public class CatalogTypeServiceTests
{
    private readonly Mock<ICatalogTypeRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CatalogTypeService>> _loggerMock;
    private readonly CatalogTypeService _service;

    public CatalogTypeServiceTests()
    {
        _repositoryMock = new Mock<ICatalogTypeRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CatalogTypeService>>();
        _service = new CatalogTypeService(_repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
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
            new CatalogTypeService(null, _mapperMock.Object, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullMapper_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogTypeService(_repositoryMock.Object, null, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogTypeService(_repositoryMock.Object, _mapperMock.Object, null));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedTypes()
    {
        var types = new List<CatalogType> { new CatalogType { Id = 1, Type = "Electronics" } };
        var typeDtos = new List<CatalogTypeDto> { new CatalogTypeDto { Id = 1, Type = "Electronics" } };
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(types);
        _mapperMock.Setup(m => m.Map<IEnumerable<CatalogTypeDto>>(types)).Returns(typeDtos);

        var result = await _service.GetAllAsync();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Electronics", result.First().Type);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnMappedType()
    {
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };
        var typeDto = new CatalogTypeDto { Id = 1, Type = "Electronics" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(catalogType);
        _mapperMock.Setup(m => m.Map<CatalogTypeDto>(catalogType)).Returns(typeDto);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Electronics", result.Type);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((CatalogType)null);

        var result = await _service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedType()
    {
        var createDto = new CatalogTypeCreateDto { Type = "Books" };
        var catalogType = new CatalogType { Type = "Books" };
        var createdType = new CatalogType { Id = 1, Type = "Books" };
        var typeDto = new CatalogTypeDto { Id = 1, Type = "Books" };
        _mapperMock.Setup(m => m.Map<CatalogType>(createDto)).Returns(catalogType);
        _repositoryMock.Setup(r => r.AddAsync(catalogType, It.IsAny<CancellationToken>())).ReturnsAsync(createdType);
        _mapperMock.Setup(m => m.Map<CatalogTypeDto>(createdType)).Returns(typeDto);

        var result = await _service.CreateAsync(createDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Books", result.Type);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingId_ShouldReturnUpdatedType()
    {
        var updateDto = new CatalogTypeUpdateDto { Type = "UpdatedType" };
        var existingType = new CatalogType { Id = 1, Type = "OriginalType" };
        var updatedDto = new CatalogTypeDto { Id = 1, Type = "UpdatedType" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existingType);
        _mapperMock.Setup(m => m.Map(updateDto, existingType)).Returns(existingType);
        _mapperMock.Setup(m => m.Map<CatalogTypeDto>(existingType)).Returns(updatedDto);

        var result = await _service.UpdateAsync(1, updateDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ShouldThrowKeyNotFoundException()
    {
        var updateDto = new CatalogTypeUpdateDto { Type = "UpdatedType" };
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((CatalogType)null);

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
