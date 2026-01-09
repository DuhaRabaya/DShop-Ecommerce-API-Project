using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface ICartService
    {
        Task<BaseResponse> AddToCartAsync(string userId,AddToCartRequest request);
        Task<CartSummaryResponse> getItems(string userId, string lang = "en");
        Task<BaseResponse> clearCart(string userId);
    }
}
