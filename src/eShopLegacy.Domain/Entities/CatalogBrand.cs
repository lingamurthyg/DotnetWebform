using System.ComponentModel.DataAnnotations;

namespace eShopLegacy.Domain.Entities;

/// <summary>
/// Catalog brand entity
/// </summary>
public class CatalogBrand
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Brand { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    public ICollection<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
}
