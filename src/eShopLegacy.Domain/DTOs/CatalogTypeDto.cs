namespace eShopLegacy.Domain.DTOs;

/// <summary>
/// DTO for CatalogType
/// </summary>
public class CatalogTypeDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// DTO for creating a CatalogType
/// </summary>
public class CatalogTypeCreateDto
{
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// DTO for updating a CatalogType
/// </summary>
public class CatalogTypeUpdateDto
{
    public string Type { get; set; } = string.Empty;
}
