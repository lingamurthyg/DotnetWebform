namespace eShopLegacy.Domain.DTOs;

/// <summary>
/// DTO for CatalogBrand
/// </summary>
public class CatalogBrandDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
}

/// <summary>
/// DTO for creating a CatalogBrand
/// </summary>
public class CatalogBrandCreateDto
{
    public string Brand { get; set; } = string.Empty;
}

/// <summary>
/// DTO for updating a CatalogBrand
/// </summary>
public class CatalogBrandUpdateDto
{
    public string Brand { get; set; } = string.Empty;
}
