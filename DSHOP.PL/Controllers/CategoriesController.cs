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

namespace DSHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ApplicationDbContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        [HttpGet("")]
        public IActionResult Index() {
            var cats= _context.Categories.Include(c=>c.Translations).ToList();
            var response= cats.Adapt<List<CategoryResponse>>();

          //  return Ok(_localizer["Success"]);
            return Ok(new { message = _localizer["Success"].Value , response});
        }
        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var cat = request.Adapt<Category>();
            _context.Categories.Add(cat);
            _context.SaveChanges();

            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
