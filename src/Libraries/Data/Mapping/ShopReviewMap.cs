using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ShopReviewMap : MappingEntityTypeConfiguration<ShopReview>
{
    public override void Configure(EntityTypeBuilder<ShopReview> builder)
    {
        builder.ToTable("ShopReviews");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ShopId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.CustomerId).HasColumnType("int");
        builder.Property(p => p.Rating).HasColumnType("int");
        builder.Property(p => p.Review).HasMaxLength(255);
        builder.Property(p => p.Reply).HasMaxLength(255);
    }
}