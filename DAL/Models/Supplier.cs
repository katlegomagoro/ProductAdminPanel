namespace ProductAdminPanel.DAL.Models
{
    public class Supplier : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? Website { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
