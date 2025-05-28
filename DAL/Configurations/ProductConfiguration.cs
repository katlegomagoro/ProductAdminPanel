using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductAdminPanel.DAL.Models;

namespace ProductAdminPanel.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.SKU).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)");
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.CostPrice).HasPrecision(18, 2);
            builder.Property(p => p.Status).HasConversion<string>();

            builder.HasOne(p => p.Supplier)
                   .WithMany(s => s.Products)
                   .HasForeignKey(p => p.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
