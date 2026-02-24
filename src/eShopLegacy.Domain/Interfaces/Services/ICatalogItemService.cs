using eShopLegacy.Domain.DTOs;

namespace eShopLegacy.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogItem operations
/// </summary>
public interface ICatalogItemService
{
    Task<IEnumerable<CatalogItemDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItemDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PaginatedItemsDto<CatalogItemDto>> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<CatalogItemDto> CreateAsync(CatalogItemCreateDto dto, CancellationToken cancellationToken = default);
    Task<CatalogItemDto> UpdateAsync(int id, CatalogItemUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItemDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
