using DSHOP.DAL.Data;
using DSHOP.DAL.Models;
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
    }
}
