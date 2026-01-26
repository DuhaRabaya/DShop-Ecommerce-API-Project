using DSHOP.DAL.Data;
using DSHOP.DAL.Migrations;
using DSHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order request)
        {
            await _context.Orders.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Order> GetBySessionId(string sessionId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.SessionId == sessionId);
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders.
                 Include(o=>o.User)
                .Include(o=>o.Items)
                .ThenInclude(o=>o.Product)
                .FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<List<Order>> GetOrdersByStatus(OrderStatusEnum orderStatus)
        {
            return await _context.Orders.Where(o=>o.OrderStatus == orderStatus).Include(o=>o.User).ToListAsync();
        }

        public async Task<Order> UpdateAsync(Order request)
        {
            _context.Orders.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }
        public async Task<bool> ProductIsDeliveredToUser(string userId, int productId)
        {
            return await _context.Orders.Where(o => o.UserId == userId && o.OrderStatus == OrderStatusEnum.Delivered)
                .SelectMany(o=>o.Items)
                .AnyAsync(o=>o.ProductId == productId);         
        }
    }
}
