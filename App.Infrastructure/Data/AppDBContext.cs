using App.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace App.Infrastructure.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            base.Database.Migrate();
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<AppSetting> AppSetting{ get; set; }
        public DbSet<Broker> Broker { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Stock> Stock { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            modelBuilder.Entity<AppSetting>().HasQueryFilter(e => e.RecordStatus == Core.Entities.Base.RecordStatus.Enabled);
            modelBuilder.Entity<Broker>().HasQueryFilter(e => e.RecordStatus == Core.Entities.Base.RecordStatus.Enabled  );
             modelBuilder.Entity<Stock>().HasQueryFilter(e => e.RecordStatus == Core.Entities.Base.RecordStatus.Enabled);
           modelBuilder.Entity<Order>().HasQueryFilter(e => e.RecordStatus == Core.Entities.Base.RecordStatus.Enabled);
            modelBuilder.Entity<Person>().HasQueryFilter(e => e.RecordStatus == Core.Entities.Base.RecordStatus.Enabled);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
        }

    }
}
