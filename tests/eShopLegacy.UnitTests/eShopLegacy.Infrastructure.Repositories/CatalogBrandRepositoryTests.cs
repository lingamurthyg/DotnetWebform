using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using eShopLegacy.Domain.Entities;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Repositories;

namespace eShopLegacy.Infrastructure.Repositories.Tests;

public class CatalogBrandRepositoryTests
{
    private readonly Mock<ILogger<CatalogBrandRepository>> _loggerMock;
    private readonly DbContextOptions<CatalogContext> _options;

    public CatalogBrandRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CatalogBrandRepository>>();
        _options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        Assert.NotNull(repository);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new CatalogBrandRepository(null, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        using var context = new CatalogContext(_options);

        Assert.Throws<ArgumentNullException>(() =>
            new CatalogBrandRepository(context, null));
    }

    [Fact]
    public async Task AddAsync_ShouldAddBrandToDatabase()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var brand = new CatalogBrand { Brand = "TestBrand" };

        var result = await repository.AddAsync(brand);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("TestBrand", result.Brand);
        Assert.True(result.IsActive);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnBrand()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var brand = new CatalogBrand { Brand = "TestBrand" };
        await repository.AddAsync(brand);

        var result = await repository.GetByIdAsync(brand.Id);

        Assert.NotNull(result);
        Assert.Equal(brand.Id, result.Id);
        Assert.Equal("TestBrand", result.Brand);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        var result = await repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActiveBrands()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        await repository.AddAsync(new CatalogBrand { Brand = "Brand1" });
        await repository.AddAsync(new CatalogBrand { Brand = "Brand2" });

        var result = await repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateBrandInDatabase()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var brand = new CatalogBrand { Brand = "OriginalBrand" };
        await repository.AddAsync(brand);

        brand.Brand = "UpdatedBrand";
        await repository.UpdateAsync(brand);

        var updated = await repository.GetByIdAsync(brand.Id);
        Assert.Equal("UpdatedBrand", updated.Brand);
        Assert.NotNull(updated.ModifiedDate);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteBrand()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var brand = new CatalogBrand { Brand = "TestBrand" };
        await repository.AddAsync(brand);

        await repository.DeleteAsync(brand.Id);

        var result = await repository.GetByIdAsync(brand.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingId_ShouldNotThrowException()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        await repository.DeleteAsync(999);

        Assert.True(true);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var brand = new CatalogBrand { Brand = "TestBrand" };
        await repository.AddAsync(brand);

        var result = await repository.ExistsAsync(brand.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        var result = await repository.ExistsAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task AddAsync_ShouldSetCreatedDateToUtcNow()
    {
        using var context = new CatalogContext(_options);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var brand = new CatalogBrand { Brand = "TestBrand" };
        var beforeAdd = DateTime.UtcNow;

        var result = await repository.AddAsync(brand);

        Assert.True(result.CreatedDate >= beforeAdd);
        Assert.True(result.CreatedDate <= DateTime.UtcNow);
    }
}
