using App.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace App.Repositories
{ 
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StokHareket> StokHareketleri { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Stock>()
           .Property(p => p.Id)
           .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<StokHareket>()
           .Property(p => p.Id)
           .HasDefaultValueSql("NEWID()");
            base.OnModelCreating(modelBuilder);
        }
    }
}