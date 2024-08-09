using Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterEntityMapping(modelBuilder);
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