// DSHOP.BLL/Service/IProductService.cs
using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(ProductRequest request);
        Task<List<ProductResponse>> GetAllAsyncForAdmin();
    }
}
