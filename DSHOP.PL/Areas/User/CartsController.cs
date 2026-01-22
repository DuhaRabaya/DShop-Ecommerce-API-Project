using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
using DSHOP.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace DSHOP.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CartsController(ICartService cartService, IStringLocalizer<SharedResource> localizer)
        {
            _cartService = cartService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var user=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result=await _cartService.AddToCartAsync(user, request);
            return Ok(result);
        }
        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery]string lang="en")
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.getItems(user ,lang);
            return Ok(result);
        }
        [HttpDelete("")]
        public async Task<IActionResult> clearCart()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.clearCart(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result= await _cartService.RemoveFromCart(user,id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuantity([FromRoute] int id,[FromBody]UpdateQuantityRequest request)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.UpdateQuantity(user, id ,request.Count);
            if(!result.Success) return BadRequest(result);
            return Ok(result);
        }


    } 
}
