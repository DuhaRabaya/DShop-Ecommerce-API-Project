using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using DSHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }

        public async Task<List<OrderResponse>> GetOrders(OrderStatusEnum status)
        {
             return (await _orderRepository.GetOrdersByStatus(status)).Adapt<List<OrderResponse>>();        
        }

        public async Task<BaseResponse> UpdateOrderStatus(int orderId, OrderStatusEnum newStatus)
        {
            var order=await _orderRepository.GetOrderById(orderId);
            if(order is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "order not found!"
                };
            }
            order.OrderStatus = newStatus;
            if (newStatus == OrderStatusEnum.Delivered)
            {
                order.PaymentStatus = PaymentStatusEnum.Paid;
            }
            else if (newStatus == OrderStatusEnum.Cancelled)
            {
                if (order.OrderStatus == OrderStatusEnum.Shipped)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "order can't be cancelled, it's already shipped"
                    };  
                }
            }
            await _orderRepository.UpdateAsync(order);
            return new BaseResponse()
            {
                Success = true,
                Message = "status updated successfully"
            };
        }
    }
}
