using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ShopMap : MappingEntityTypeConfiguration<Shop>
{
    public override void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shops");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(128);
        builder.Property(p => p.Description).HasMaxLength(255);
        builder.Property(p => p.ProfileImage).HasMaxLength(255);
        builder.Property(p => p.ServiceDescription).HasMaxLength(255);
        builder.Property(p => p.Website).HasMaxLength(255);
        builder.Property(p => p.Address).HasMaxLength(255);
        builder.Property(p => p.City).HasMaxLength(255);
        builder.Property(p => p.Country).HasMaxLength(255);
        builder.Property(p => p.PostalCode).HasMaxLength(10);
        builder.Property(p => p.Phone).HasMaxLength(20);
        builder.Property(p => p.Email).HasMaxLength(255);
        builder.Property(p => p.AverageRating).HasPrecision(2, 1);
        builder.Property(p => p.TotalReviews).HasColumnType("int");
        builder.Property(p => p.OwnerId).HasColumnType("int");
        // bool type in sql server
        builder.Property(p => p.IsVerified).HasDefaultValue(false);
        builder.Property(p => p.IsActive).HasDefaultValue(true);
        
        builder.HasIndex(p => p.Name).IsUnique();
        builder.HasIndex(p => p.Email).IsUnique();
        
        builder.OwnsMany(shop => shop.OpeningHours, builderIn => { builderIn.ToJson(); });
        builder.OwnsMany(shop => shop.Gallery, builderIn => { builderIn.ToJson(); });
        builder.OwnsMany(shop => shop.SocialMedia, builderIn => { builderIn.ToJson(); });
        
        builder.HasMany(p => p.ShopServices)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ShopSettings)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Appointments)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Employees)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.WaitLists)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.Reviews)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}