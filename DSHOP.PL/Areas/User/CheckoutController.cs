using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DSHOP.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Payment([FromBody] CheckoutRequest request)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkoutService.PaymentProcess(request,user);

            if (!response.Success) { 
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("success")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromQuery] string session_id)
        {
            var response= await _checkoutService.HandleSuccess(session_id);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
