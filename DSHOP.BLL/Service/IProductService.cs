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
        Task<PaginateResponse<ProductUserResponse>> GetAllAsyncForUser(string lang = "en",
             int page = 1, int limit = 1, string? search = null,
             int? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null,
             string? sortBy = null, bool asc = true);
        Task<ProductUserDetails> GetProductDetailsForUser(int id, string lang = "en");
        Task<BaseResponse> DeleteProductAsync(int productId);
        Task<BaseResponse> UpdateProductAsync(int productId, UpdateProductRequest request);
    }
}
