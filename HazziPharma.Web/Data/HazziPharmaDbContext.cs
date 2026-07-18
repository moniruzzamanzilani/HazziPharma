using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HazziPharma.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Data
{
    public class HazziPharmaDbContext : IdentityDbContext<ApplicationUser>
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }

        public DbSet<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .Property(p => p.PurchasePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.SalePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseDetail>()
                .Property(p => p.PurchasePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseDetail>()
                .Property(p => p.SubTotal)
                .HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
    }
}