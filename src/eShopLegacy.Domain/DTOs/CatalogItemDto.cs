namespace eShopLegacy.Domain.DTOs;

/// <summary>
/// DTO for CatalogItem
/// </summary>
public class CatalogItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureFileName { get; set; }
    public int CatalogTypeId { get; set; }
    public string? CatalogTypeName { get; set; }
    public int CatalogBrandId { get; set; }
    public string? CatalogBrandName { get; set; }
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public bool OnReorder { get; set; }
}

/// <summary>
/// DTO for creating a CatalogItem
/// </summary>
public class CatalogItemCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureFileName { get; set; }
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public bool OnReorder { get; set; }
}

/// <summary>
/// DTO for updating a CatalogItem
/// </summary>
public class CatalogItemUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureFileName { get; set; }
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public bool OnReorder { get; set; }
}
