using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSHOP.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ManagerController : ControllerBase
    {
        private readonly IManageUserService _manageUserService;

        public ManagerController(IManageUserService manageUserService)
        {
            _manageUserService = manageUserService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetUsers()=>Ok(await _manageUserService.GetUsers());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails([FromRoute]string id)
        {
            var response = await _manageUserService.GetUserDetails(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPatch("block/{id}")]
        public async Task<IActionResult> BlockUser([FromRoute] string id)
        {
            var response = await _manageUserService.BlockUser(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPatch("unblock/{id}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string id)
        {
            var response = await _manageUserService.UnBLockUser(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPatch("change-role")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeUserRoleRequest request) { 
            var response = await _manageUserService.ChangeUserRoLe(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);

        }
    }
}
