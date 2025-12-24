using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
using DSHOP.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DSHOP.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ICategoryService categoryService , IStringLocalizer<SharedResource> localizer )
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery]string lang="en")
        {
            var cats =await _categoryService.GetAllAsyncForUser(lang);
            return Ok(new { Message = _localizer["Success"].Value, cats });
        }
        
    }
}
