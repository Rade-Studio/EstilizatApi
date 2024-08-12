using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class AppointmentMap : MappingEntityTypeConfiguration<Appointment>
{
    public override void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ShopId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.CustomerId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.ServiceId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.EmployeeId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.CancelReason).HasMaxLength(255);
        builder.Property(p => p.PaymentStatus).HasConversion<int>();
        builder.Property(p => p.Status).HasConversion<int>();
        builder.Property(p => p.ReminderSent).HasDefaultValue(false);

        builder.HasOne(p => p.ShopService)
            .WithMany(p => p.Appointments)
            .HasForeignKey(p => p.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}