using HazziPharma.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Data
{
    public class HazziPharmaDbContext : DbContext
    {
        public HazziPharmaDbContext(DbContextOptions<HazziPharmaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Generic> Generics { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}