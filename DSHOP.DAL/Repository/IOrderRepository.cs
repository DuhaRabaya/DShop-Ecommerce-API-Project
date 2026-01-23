using DSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order request);
        Task<Order> GetBySessionId(string sessionId);
        Task<Order> UpdateAsync(Order request);

        Task<List<Order>> GetOrdersByStatus(OrderStatusEnum orderStatus);
        Task<Order?> GetOrderById(int id);



    }
}
