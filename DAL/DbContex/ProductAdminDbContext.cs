using Microsoft.EntityFrameworkCore;
using ProductAdminPanel.DAL.Models;

namespace ProductAdminPanel.DAL.DbContex
{
    public class ProductAdminDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ProductAdminDbContext(DbContextOptions<ProductAdminDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductAdminDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
