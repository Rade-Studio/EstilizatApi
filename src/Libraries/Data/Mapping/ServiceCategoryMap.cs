using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ServiceCategoryMap : MappingEntityTypeConfiguration<ServiceCategory>
{
    public override void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.ToTable("ServiceCategories");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(128);
        builder.Property(p => p.Description).HasMaxLength(255);
        
        builder.HasMany(p => p.ShopServices)
            .WithOne(p => p.ServiceCategory)
            .HasForeignKey(p => p.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}