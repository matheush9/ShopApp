using Microsoft.EntityFrameworkCore;

namespace ShopApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Cart> Carts => Set<Cart>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(i => i.Items)
                .WithOne(p => p.Product);

            modelBuilder.Entity<Order>()
                .HasMany(i => i.Items)
                .WithOne(o => o.Order)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<Cart>()
                .HasMany(i => i.Items)
                .WithOne(c => c.Cart)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
