using ProductAdminPanel.DAL.DbContex;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ProductAdminPanel.Services
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
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
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
