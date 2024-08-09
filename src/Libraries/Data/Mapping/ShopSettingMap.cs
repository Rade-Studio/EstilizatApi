using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ShopSettingMap : MappingEntityTypeConfiguration<ShopSetting>
{
    public override void Configure(EntityTypeBuilder<ShopSetting> builder)
    {
        builder.ToTable("ShopSettings");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.ShopId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Key).HasMaxLength(256);
        builder.Property(p => p.Value).HasMaxLength(256);
    }
}