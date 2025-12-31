using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSHOP.DAL.Data;
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
    }
}
