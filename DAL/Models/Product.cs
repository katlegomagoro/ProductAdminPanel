using ProductAdminPanel.DAL.Enums;

namespace ProductAdminPanel.DAL.Models
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public int StockQuantity { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime? LaunchDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = default!;
    }
}
