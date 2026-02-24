using eShopLegacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopLegacy.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for CatalogType entity
/// </summary>
public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.ToTable("CatalogType");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(ct => ct.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ct => ct.CreatedDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

        builder.Property(ct => ct.ModifiedDate)
            .HasColumnType("timestamp without time zone");

        builder.Property(ct => ct.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(ct => ct.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ct => ct.ModifiedBy)
            .HasMaxLength(100);

        builder.HasIndex(ct => ct.Type);
    }
}
