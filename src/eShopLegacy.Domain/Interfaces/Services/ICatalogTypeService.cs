using eShopLegacy.Domain.DTOs;

namespace eShopLegacy.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogType operations
/// </summary>
public interface ICatalogTypeService
{
    Task<IEnumerable<CatalogTypeDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogTypeDto> CreateAsync(CatalogTypeCreateDto dto, CancellationToken cancellationToken = default);
    Task<CatalogTypeDto> UpdateAsync(int id, CatalogTypeUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
