// DSHOP.BLL/Service/IProductService.cs
using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(ProductRequest request);
        Task<List<ProductResponse>> GetAllAsyncForAdmin();
        Task<List<ProductUserResponse>> GetAllAsyncForUser([FromQuery] string lang = "en", int page = 1, int limit = 1 , string? search=null);
        Task<ProductUserDetails> GetProductDetailsForUser(int id, string lang = "en");
    }
}
