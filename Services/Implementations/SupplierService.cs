using ProductAdminPanel.DAL.DbContex;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ProductAdminPanel.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ProductAdminDbContext _context;

        public SupplierService(ProductAdminDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetAllAsync() =>
            await _context.Suppliers.ToListAsync();

        public async Task<Supplier?> GetByIdAsync(Guid id) =>
            await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            var existing = await _context.Suppliers.FindAsync(supplier.Id);
            if (existing != null)
            {
                existing.Name = supplier.Name;
                existing.ContactEmail = supplier.ContactEmail;
                existing.ContactNumber = supplier.ContactNumber;
                existing.Website = supplier.Website;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }
    }
}
