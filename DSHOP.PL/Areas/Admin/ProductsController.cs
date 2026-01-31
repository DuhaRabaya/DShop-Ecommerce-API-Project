using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductsController(IProductService productService , IStringLocalizer<SharedResource> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromForm]ProductRequest request)
        {
            var response=await _productService.CreateProductAsync(request);
            return Ok(new { message = _localizer["Success"].Value , response});
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsyncForAdmin();
            return Ok(new { Message = _localizer["Success"].Value, products });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var response = await _productService.DeleteProductAsync(id);

            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm]UpdateProductRequest request)
        {
            var response = await _productService.UpdateProductAsync(id, request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
