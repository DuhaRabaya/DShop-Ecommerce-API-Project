using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSHOP.DAL.Data;
using Microsoft.Extensions.Localization;
using DSHOP.PL.Resources;
using Mapster;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using DSHOP.DAL.Repository;
using DSHOP.BLL.Service;

namespace DSHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IStringLocalizer<SharedResource> localizer , ICategoryService categoryService)
        {
            _localizer = localizer;
            _categoryService=categoryService;
        }
        [HttpGet("")]
        public IActionResult Index() {
            var cats=_categoryService.GetAll();
          //  return Ok(_localizer["Success"]);
            return Ok(new { message = _localizer["Success"].Value , cats});
        }
        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
           var response= _categoryService.Create(request);

            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
