using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogItem entity
/// </summary>
public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly CatalogContext _context;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(CatalogContext context, ILogger<CatalogItemRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog items");
            throw;
        }
    }

    public async Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .AsNoTracking()
                .FirstOrDefaultAsync(ci => ci.Id == id && ci.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive);

            var totalCount = await query.LongCountAsync(cancellationToken);

            var items = await query
                .OrderBy(ci => ci.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving paginated catalog items");
            throw;
        }
    }

    public async Task<CatalogItem> AddAsync(CatalogItem entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;
            await _context.CatalogItems.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog item");
            throw;
        }
    }

    public async Task UpdateAsync(CatalogItem entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _context.CatalogItems.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with id {Id}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.CatalogItems.FindAsync(new object[] { id }, cancellationToken);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .AnyAsync(ci => ci.Id == id && ci.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive &&
                    (ci.Name.Contains(searchTerm) ||
                     (ci.Description != null && ci.Description.Contains(searchTerm))))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items");
            throw;
        }
    }
}
