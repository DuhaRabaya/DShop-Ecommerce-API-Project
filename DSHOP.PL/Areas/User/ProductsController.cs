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
    
    public class ProductsController : ControllerBase
    {
      
            private readonly IProductService _productService;
            private readonly IStringLocalizer<SharedResource> _localizer;
            private readonly IReviewService _reviewService;

        public ProductsController(IProductService productService, IStringLocalizer<SharedResource> localizer,IReviewService reviewService)
            {
                _productService = productService;
                _localizer = localizer;
            _reviewService = reviewService;
        }
            [HttpGet("")]
            public async Task<IActionResult> Index([FromQuery] string lang = "en" , [FromQuery]int page=1,
                [FromQuery] int limit=5, [FromQuery] string? search=null , [FromQuery] int? categoryId=null 
                , [FromQuery] decimal? minPrice=null , [FromQuery] decimal? maxPrice=null,
                [FromQuery] string? sortBy = null, [FromQuery] bool asc = true)
            {
                var products = await _productService.GetAllAsyncForUser(lang ,page ,limit,search,categoryId,minPrice,maxPrice,sortBy,asc);
                return Ok(new { Message = _localizer["Success"].Value, products });
            }
        [HttpGet("{id}")]
        public async Task<IActionResult> Index([FromRoute]int id ,[FromQuery] string lang = "en")
        {
            var products = await _productService.GetProductDetailsForUser(id,lang);
            return Ok(new { Message = _localizer["Success"].Value, products });
        }
        [HttpPost("review/{productId}")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest request, [FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _reviewService.AddReview(userId, productId, request);
            if(!response.Success)return BadRequest(response);
            return Ok(response);
        }

    }
    }
