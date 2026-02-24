using eShopLegacy.Domain.DTOs;

namespace eShopLegacy.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogBrand operations
/// </summary>
public interface ICatalogBrandService
{
    Task<IEnumerable<CatalogBrandDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogBrandDto> CreateAsync(CatalogBrandCreateDto dto, CancellationToken cancellationToken = default);
    Task<CatalogBrandDto> UpdateAsync(int id, CatalogBrandUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
