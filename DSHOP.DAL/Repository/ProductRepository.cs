using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSHOP.DAL.Data;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using Microsoft.EntityFrameworkCore; 

namespace DSHOP.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
           
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(c => c.Translations).Include(c=>c.User).ToListAsync();
        }

        public async Task<Product?> FindByIdAsync(int id)
        {
            return await _context.Products.Include(c => c.Translations).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DecreaseQuantities(List<(int ProductId , int Quantitiy)> items)
        {
            var productIds= items.Select(p => p.ProductId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            foreach (var product in products) {
                var item= items.FirstOrDefault(p=>p.ProductId == product.Id);
                if (product.Quantity < item.Quantitiy)
                {
                    return false;
                }
                product.Quantity-=item.Quantitiy;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public IQueryable<Product> Query() {
            return _context.Products.Include(p=>p.Translations).AsQueryable();
        }

    }
}
