using Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Models.DbEntities;

namespace Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterEntityMapping(modelBuilder);
            
            // adding composite key for EmployeeSkill table
            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(p => new { p.EmployeeId, p.ShopServiceId });
            
            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.Services)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(p => p.Service)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.ShopServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            
            base.OnModelCreating(modelBuilder);
        }

        public void RegisterEntityMapping(ModelBuilder modelBuilder)
        {
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false) &&
                (type.BaseType.GetGenericTypeDefinition() == typeof(MappingEntityTypeConfiguration<>))
            );
            foreach (var item in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(item);
                configuration!.ApplyConfiguration(modelBuilder);
            }
        }

        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}