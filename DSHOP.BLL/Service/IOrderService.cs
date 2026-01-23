using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetOrders(OrderStatusEnum status);
        Task<BaseResponse> UpdateOrderStatus(int orderId, OrderStatusEnum newStatus);
        Task<Order?> GetOrderById(int orderId);
    }
}
