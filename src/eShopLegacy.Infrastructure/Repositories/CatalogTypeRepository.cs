using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Infrastructure.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogContext _context;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(CatalogContext context, ILogger<CatalogTypeRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .Where(ct => ct.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog types");
            throw;
        }
    }

    public async Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id && ct.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogType> AddAsync(CatalogType entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;
            await _context.CatalogTypes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog type");
            throw;
        }
    }

    public async Task UpdateAsync(CatalogType entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _context.CatalogTypes.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type with id {Id}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.CatalogTypes.FindAsync(new object[] { id }, cancellationToken);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AnyAsync(ct => ct.Id == id && ct.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of catalog type with id {Id}", id);
            throw;
        }
    }
}
