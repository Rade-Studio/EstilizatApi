using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ShopServiceMap : MappingEntityTypeConfiguration<ShopService>
{
    public override void Configure(EntityTypeBuilder<ShopService> builder)
    {
        builder.ToTable("ShopServices");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(128);
        builder.Property(p => p.Description).HasMaxLength(255);
        builder.Property(p => p.Price).HasPrecision(2,1);
        builder.Property(p => p.Discount).HasPrecision(2, 1);
        builder.Property(p => p.ShopId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.ServiceCategoryId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Duration).HasColumnType("int");
        
        builder.OwnsMany(shop => shop.Materials, builderIn => { builderIn.ToJson(); });
        builder.OwnsMany(shop => shop.IncludeProducts, builderIn => { builderIn.ToJson(); });
    }
}