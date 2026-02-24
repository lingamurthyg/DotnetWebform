using System.ComponentModel.DataAnnotations;

namespace eShopLegacy.Domain.Entities;

/// <summary>
/// Catalog item entity representing a product in the catalog
/// </summary>
public class CatalogItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(0, 9999999999999999.99)]
    public decimal Price { get; set; }

    [MaxLength(200)]
    public string? PictureFileName { get; set; }

    public int CatalogTypeId { get; set; }
    public CatalogType? CatalogType { get; set; }

    public int CatalogBrandId { get; set; }
    public CatalogBrand? CatalogBrand { get; set; }

    [Range(0, 10000000)]
    public int AvailableStock { get; set; }

    [Range(0, 10000000)]
    public int RestockThreshold { get; set; }

    [Range(0, 10000000)]
    public int MaxStockThreshold { get; set; }

    public bool OnReorder { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }
}
