using BakeryPOS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryPOS.Core.Data;

public class BakeryDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();
    public DbSet<CashCut> CashCuts => Set<CashCut>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=bakery_pos.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Precios e importes con precisión decimal
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Product>().Property(p => p.StockQuantity).HasPrecision(18, 3);
        modelBuilder.Entity<Sale>().Property(s => s.Total).HasPrecision(18, 2);
        modelBuilder.Entity<SaleDetail>().Property(sd => sd.Quantity).HasPrecision(18, 3);
    }
}