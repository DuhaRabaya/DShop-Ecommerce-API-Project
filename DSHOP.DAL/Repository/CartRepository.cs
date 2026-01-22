using DSHOP.DAL.Data;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> AddAsync(Cart request)
        {
            await _context.Carts.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Cart?> getItem(string userId, int productId)
        { 
            return await _context.Carts.Include(c=>c.Product).
                FirstOrDefaultAsync(c=>c.UserId == userId && c.ProductId==productId)
                ;
        }

        public async Task<List<Cart>> getItems(string userId)
        {
            return await _context.Carts.Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ThenInclude(c => c.Translations).ToListAsync();
        }

        public async Task<Cart> updateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task clearCart(string userId)
        {
            var cart= await _context.Carts.Where(c=>c.UserId == userId).ToListAsync();
            _context.RemoveRange(cart);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Cart cart)
        {
            _context.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }
}
