using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Repositories;

namespace eShopLegacy.Infrastructure.Repositories.Tests;

public class CatalogItemRepositoryTests
{
    private readonly Mock<ILogger<CatalogItemRepository>> _loggerMock;
    private readonly DbContextOptions<CatalogContext> _options;

    public CatalogItemRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CatalogItemRepository>>();
        _options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        Assert.NotNull(repository);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemRepository(null, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        using var context = new CatalogContext(_options);

        Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemRepository(context, null));
    }

    [Fact]
    public async Task AddAsync_ShouldAddItemToDatabase()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        var item = new CatalogItem
        {
            Name = "TestItem",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };

        var result = await repository.AddAsync(item);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("TestItem", result.Name);
        Assert.True(result.IsActive);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnItem()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        var item = new CatalogItem
        {
            Name = "TestItem",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };
        await repository.AddAsync(item);

        var result = await repository.GetByIdAsync(item.Id);

        Assert.NotNull(result);
        Assert.Equal(item.Id, result.Id);
        Assert.Equal("TestItem", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var result = await repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActiveItems()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        await repository.AddAsync(new CatalogItem
        {
            Name = "Item1",
            Price = 10m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        });
        await repository.AddAsync(new CatalogItem
        {
            Name = "Item2",
            Price = 20m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        });

        var result = await repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateItemInDatabase()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        var item = new CatalogItem
        {
            Name = "OriginalName",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };
        await repository.AddAsync(item);

        item.Name = "UpdatedName";
        await repository.UpdateAsync(item);

        var updated = await repository.GetByIdAsync(item.Id);
        Assert.Equal("UpdatedName", updated.Name);
        Assert.NotNull(updated.ModifiedDate);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteItem()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        var item = new CatalogItem
        {
            Name = "TestItem",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };
        await repository.AddAsync(item);

        await repository.DeleteAsync(item.Id);

        var result = await repository.GetByIdAsync(item.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingId_ShouldNotThrowException()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        await repository.DeleteAsync(999);

        Assert.True(true);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        var item = new CatalogItem
        {
            Name = "TestItem",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };
        await repository.AddAsync(item);

        var result = await repository.ExistsAsync(item.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var result = await repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task GetPaginatedAsync_ShouldReturnPagedResults()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        for (int i = 1; i <= 25; i++)
        {
            await repository.AddAsync(new CatalogItem
            {
                Name = $"Item{i}",
                Price = i,
                CatalogTypeId = 1,
                CatalogBrandId = 1
            });
        }

        var (items, totalCount) = await repository.GetPaginatedAsync(0, 10);

        Assert.Equal(10, items.Count());
        Assert.Equal(25, totalCount);
    }

    [Fact]
    public async Task SearchAsync_WithMatchingTerm_ShouldReturnMatchingItems()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        await repository.AddAsync(new CatalogItem
        {
            Name = "TestProduct",
            Description = "Description",
            Price = 10m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        });
        await repository.AddAsync(new CatalogItem
        {
            Name = "AnotherItem",
            Description = "TestDescription",
            Price = 20m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        });

        var result = await repository.SearchAsync("Test");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchAsync_WithEmptyString_ShouldReturnAllItems()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        await repository.AddAsync(new CatalogItem
        {
            Name = "Item1",
            Price = 10m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        });

        var result = await repository.SearchAsync("");

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task AddAsync_ShouldSetCreatedDateToUtcNow()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        var item = new CatalogItem
        {
            Name = "TestItem",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };
        var beforeAdd = DateTime.UtcNow;

        var result = await repository.AddAsync(item);

        Assert.True(result.CreatedDate >= beforeAdd);
        Assert.True(result.CreatedDate <= DateTime.UtcNow);
    }
}
