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

public class CatalogItemServiceTests
{
    private readonly Mock<ICatalogItemRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CatalogItemService>> _loggerMock;
    private readonly CatalogItemService _service;

    public CatalogItemServiceTests()
    {
        _repositoryMock = new Mock<ICatalogItemRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CatalogItemService>>();
        _service = new CatalogItemService(_repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
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
            new CatalogItemService(null, _mapperMock.Object, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullMapper_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemService(_repositoryMock.Object, null, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemService(_repositoryMock.Object, _mapperMock.Object, null));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedItems()
    {
        var items = new List<CatalogItem> { new CatalogItem { Id = 1, Name = "Item1" } };
        var itemDtos = new List<CatalogItemDto> { new CatalogItemDto { Id = 1, Name = "Item1" } };
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(items);
        _mapperMock.Setup(m => m.Map<IEnumerable<CatalogItemDto>>(items)).Returns(itemDtos);

        var result = await _service.GetAllAsync();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Item1", result.First().Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnMappedItem()
    {
        var item = new CatalogItem { Id = 1, Name = "TestItem" };
        var itemDto = new CatalogItemDto { Id = 1, Name = "TestItem" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _mapperMock.Setup(m => m.Map<CatalogItemDto>(item)).Returns(itemDto);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("TestItem", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((CatalogItem)null);

        var result = await _service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetPaginatedAsync_ShouldReturnPaginatedResult()
    {
        var items = new List<CatalogItem> { new CatalogItem { Id = 1, Name = "Item1" } };
        var itemDtos = new List<CatalogItemDto> { new CatalogItemDto { Id = 1, Name = "Item1" } };
        _repositoryMock.Setup(r => r.GetPaginatedAsync(0, 10, It.IsAny<CancellationToken>()))
            .ReturnsAsync((items, 25L));
        _mapperMock.Setup(m => m.Map<IEnumerable<CatalogItemDto>>(items)).Returns(itemDtos);

        var result = await _service.GetPaginatedAsync(0, 10);

        Assert.NotNull(result);
        Assert.Equal(0, result.PageIndex);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(25, result.TotalCount);
        Assert.Equal(3, result.TotalPages);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedItem()
    {
        var createDto = new CatalogItemCreateDto { Name = "NewItem", Price = 99.99m };
        var item = new CatalogItem { Name = "NewItem", Price = 99.99m };
        var createdItem = new CatalogItem { Id = 1, Name = "NewItem", Price = 99.99m };
        var itemDto = new CatalogItemDto { Id = 1, Name = "NewItem", Price = 99.99m };
        _mapperMock.Setup(m => m.Map<CatalogItem>(createDto)).Returns(item);
        _repositoryMock.Setup(r => r.AddAsync(item, It.IsAny<CancellationToken>())).ReturnsAsync(createdItem);
        _mapperMock.Setup(m => m.Map<CatalogItemDto>(createdItem)).Returns(itemDto);

        var result = await _service.CreateAsync(createDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("NewItem", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingId_ShouldReturnUpdatedItem()
    {
        var updateDto = new CatalogItemUpdateDto { Name = "UpdatedItem", Price = 79.99m };
        var existingItem = new CatalogItem { Id = 1, Name = "OriginalItem", Price = 99.99m };
        var updatedDto = new CatalogItemDto { Id = 1, Name = "UpdatedItem", Price = 79.99m };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existingItem);
        _mapperMock.Setup(m => m.Map(updateDto, existingItem)).Returns(existingItem);
        _mapperMock.Setup(m => m.Map<CatalogItemDto>(existingItem)).Returns(updatedDto);

        var result = await _service.UpdateAsync(1, updateDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingId_ShouldThrowKeyNotFoundException()
    {
        var updateDto = new CatalogItemUpdateDto { Name = "UpdatedItem", Price = 79.99m };
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((CatalogItem)null);

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

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingItems()
    {
        var items = new List<CatalogItem> { new CatalogItem { Id = 1, Name = "TestItem" } };
        var itemDtos = new List<CatalogItemDto> { new CatalogItemDto { Id = 1, Name = "TestItem" } };
        _repositoryMock.Setup(r => r.SearchAsync("Test", It.IsAny<CancellationToken>())).ReturnsAsync(items);
        _mapperMock.Setup(m => m.Map<IEnumerable<CatalogItemDto>>(items)).Returns(itemDtos);

        var result = await _service.SearchAsync("Test");

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("TestItem", result.First().Name);
    }
}
