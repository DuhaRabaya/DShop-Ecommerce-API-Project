using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSHOP.PL.Areas.Identity
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token , string userId)
        {
            var result = await _authenticationService.ConfirmEmailAsync(token ,userId);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("SendCode")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _authenticationService.RequestPasswordReset(request);
            if(!result.Success) return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
        {
            var result = await _authenticationService.RequestPasswordUpdate(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        [HttpPatch("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenApiModel request)
        {
            var result=await _authenticationService.RefreshTokenAsync(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
