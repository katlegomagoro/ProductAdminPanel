using ProductAdminPanel.DAL.DbContex;
using ProductAdminPanel.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ProductAdminPanel.Services.Interfaces;

namespace ProductAdminPanel.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ProductAdminDbContext _context;

        public CategoryService(ProductAdminDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _context.Categories.Include(c => c.ParentCategory).ToListAsync();

        public async Task<Category?> GetByIdAsync(Guid id) =>
            await _context.Categories.Include(c => c.ParentCategory)
                                     .FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var existing = await _context.Categories.FindAsync(category.Id);
            if (existing != null)
            {
                existing.Name = category.Name;
                existing.Description = category.Description;
                existing.ParentCategoryId = category.ParentCategoryId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
