using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Repositories;

namespace eShopLegacy.Infrastructure.Repositories.Tests;

public class CatalogTypeRepositoryTests
{
    private readonly Mock<ILogger<CatalogTypeRepository>> _loggerMock;
    private readonly DbContextOptions<CatalogContext> _options;

    public CatalogTypeRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CatalogTypeRepository>>();
        _options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        Assert.NotNull(repository);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogTypeRepository(null, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        using var context = new CatalogContext(_options);

        Assert.Throws<ArgumentNullException>(() =>
            new CatalogTypeRepository(context, null));
    }

    [Fact]
    public async Task AddAsync_ShouldAddTypeToDatabase()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var catalogType = new CatalogType { Type = "Electronics" };

        var result = await repository.AddAsync(catalogType);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Electronics", result.Type);
        Assert.True(result.IsActive);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnType()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var catalogType = new CatalogType { Type = "Electronics" };
        await repository.AddAsync(catalogType);

        var result = await repository.GetByIdAsync(catalogType.Id);

        Assert.NotNull(result);
        Assert.Equal(catalogType.Id, result.Id);
        Assert.Equal("Electronics", result.Type);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        var result = await repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActiveTypes()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        await repository.AddAsync(new CatalogType { Type = "Type1" });
        await repository.AddAsync(new CatalogType { Type = "Type2" });

        var result = await repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTypeInDatabase()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var catalogType = new CatalogType { Type = "OriginalType" };
        await repository.AddAsync(catalogType);

        catalogType.Type = "UpdatedType";
        await repository.UpdateAsync(catalogType);

        var updated = await repository.GetByIdAsync(catalogType.Id);
        Assert.Equal("UpdatedType", updated.Type);
        Assert.NotNull(updated.ModifiedDate);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteType()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var catalogType = new CatalogType { Type = "TestType" };
        await repository.AddAsync(catalogType);

        await repository.DeleteAsync(catalogType.Id);

        var result = await repository.GetByIdAsync(catalogType.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingId_ShouldNotThrowException()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        await repository.DeleteAsync(999);

        Assert.True(true);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var catalogType = new CatalogType { Type = "TestType" };
        await repository.AddAsync(catalogType);

        var result = await repository.ExistsAsync(catalogType.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        var result = await repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task AddAsync_ShouldSetCreatedDateToUtcNow()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var catalogType = new CatalogType { Type = "TestType" };
        var beforeAdd = DateTime.UtcNow;

        var result = await repository.AddAsync(catalogType);

        Assert.True(result.CreatedDate >= beforeAdd);
        Assert.True(result.CreatedDate <= DateTime.UtcNow);
    }
}
