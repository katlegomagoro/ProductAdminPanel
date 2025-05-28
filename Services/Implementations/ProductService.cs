using ProductAdminPanel.DAL.DbContex;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ProductAdminPanel.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ProductAdminDbContext _context;

        public ProductService(ProductAdminDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _context.Products.Include(p => p.Supplier).ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id) =>
            await _context.Products.Include(p => p.Supplier)
                                   .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }

}
