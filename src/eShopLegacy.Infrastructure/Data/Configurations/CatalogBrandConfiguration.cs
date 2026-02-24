using eShopLegacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopLegacy.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for CatalogBrand entity
/// </summary>
public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable("CatalogBrand");

        builder.HasKey(cb => cb.Id);

        builder.Property(cb => cb.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(cb => cb.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cb => cb.CreatedDate)
            .IsRequired();

        builder.Property(cb => cb.ModifiedDate);

        builder.Property(cb => cb.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(cb => cb.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cb => cb.ModifiedBy)
            .HasMaxLength(100);

        builder.HasIndex(cb => cb.Brand);
    }
}
