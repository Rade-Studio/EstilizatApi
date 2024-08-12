using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class WaitListMap : MappingEntityTypeConfiguration<WaitList>
{
    public override void Configure(EntityTypeBuilder<WaitList> builder)
    {
        builder.ToTable("WaitLists");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ShopId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.CustomerId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.ServiceId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.EmployeeId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Status).HasConversion<int>();

        builder.OwnsOne(p => p.PreferredTimeRange, builderIn => { builderIn.ToJson(); });

        builder.HasOne(p => p.ShopService)
            .WithMany(p => p.WaitLists)
            .HasForeignKey(p => p.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}