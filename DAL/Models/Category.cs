namespace ProductAdminPanel.DAL.Models
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
    }
}
