using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.Models;
using DSHOP.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DSHOP.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public OrdersController(IOrderService orderService, IStringLocalizer<SharedResource> localizer)
        {
            _orderService = orderService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrders([FromQuery] OrderStatusEnum status = OrderStatusEnum.Pending)
        {
            var response= await _orderService.GetOrders(status);
            return Ok(response);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusRequest newStatus ,[FromRoute] int id)
        {
            var response = await _orderService.UpdateOrderStatus(id ,newStatus.Status);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
