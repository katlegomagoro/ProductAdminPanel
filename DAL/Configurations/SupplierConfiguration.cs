using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductAdminPanel.DAL.Models;

namespace ProductAdminPanel.DAL.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.ContactEmail).HasMaxLength(100);
            builder.Property(s => s.ContactNumber).HasMaxLength(20);
            builder.Property(s => s.Website).HasMaxLength(200);
        }
    }
}
