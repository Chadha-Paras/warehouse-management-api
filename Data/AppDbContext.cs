using Microsoft.EntityFrameworkCore;
using warehouse_management_api.Models;

namespace warehouse_management_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Warehouse>()
                .HasIndex(w => w.Name)
                .IsUnique();

            modelBuilder.Entity<Inventory>()
                .HasIndex(i => new { i.ProductId, i.WarehouseId })
                .IsUnique();

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Inventories)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Warehouse)
                .WithMany(w => w.Inventories)
                .HasForeignKey(i => i.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventory>()
                .Property(i => i.Quantity)
                .HasDefaultValue(0);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedOn = DateTime.UtcNow;
                        entity.LastChangedOn = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.LastChangedOn = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}