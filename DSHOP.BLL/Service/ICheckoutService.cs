using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface ICheckoutService
    {
        Task<CheckoutResponse> PaymentProcess(CheckoutRequest request, string UserId);
        Task<CheckoutResponse> HandleSuccess(string sessionId);
    }
}
