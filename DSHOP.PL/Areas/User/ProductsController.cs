using DSHOP.BLL.Service;
using DSHOP.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DSHOP.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
      
            private readonly IProductService _productService;
            private readonly IStringLocalizer<SharedResource> _localizer;

            public ProductsController(IProductService productService, IStringLocalizer<SharedResource> localizer)
            {
                _productService = productService;
                _localizer = localizer;
            }
            [HttpGet("")]
            public async Task<IActionResult> Index([FromQuery] string lang = "en")
            {
                var products = await _productService.GetAllAsyncForUser(lang);
                return Ok(new { Message = _localizer["Success"].Value, products });
            }
        [HttpGet("{id}")]
        public async Task<IActionResult> Index([FromRoute]int id ,[FromQuery] string lang = "en")
        {
            var products = await _productService.GetProductDetailsForUser(id,lang);
            return Ok(new { Message = _localizer["Success"].Value, products });
        }
    }
    }
