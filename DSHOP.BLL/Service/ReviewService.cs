using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using DSHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<BaseResponse> AddReview(string userId,int productId, ReviewRequest review)
        {
            var isdelivered= await _orderRepository.ProductIsDeliveredToUser(userId,productId);
            if (!isdelivered) {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "user didn't recieve the product!!"
                };
            }
           
            if (await _reviewRepository.HasReview(productId, userId))
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "user has already added a review!!"
                };
            }
            var response = review.Adapt<Review>();
            response.UserId = userId;
            response.ProductId = productId;
            await _reviewRepository.AddReview(response);

            return new BaseResponse()
            {
                Success = true,
                Message = "review added successfully"
            };
        }
       
    }
}
