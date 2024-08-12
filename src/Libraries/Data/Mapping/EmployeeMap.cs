using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class EmployeeMap : MappingEntityTypeConfiguration<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ShopId).HasColumnType("uniqueidentifier");
        builder.Property(p => p.Name).HasMaxLength(255);
        builder.Property(p => p.PhoneNumber).HasMaxLength(20);
        builder.Property(p => p.Status).HasConversion<int>();

        builder.HasMany(p => p.Appointments)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.WaitLists)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}