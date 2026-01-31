using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface IReviewService
    {
        Task<BaseResponse> AddReview(string userId, int productId, ReviewRequest review);
        Task<BaseResponse> RemoveReview(string userId, int productId);
        Task<BaseResponse> UpdateReview(string userId, int productId, ReviewRequest request);

    }
}
