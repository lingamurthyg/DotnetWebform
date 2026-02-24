namespace eShopLegacy.Domain.DTOs;

/// <summary>
/// DTO for paginated items
/// </summary>
public class PaginatedItemsDto<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; } = new List<T>();

    public bool HasPreviousPage => PageIndex > 0;
    public bool HasNextPage => PageIndex < TotalPages - 1;
}
