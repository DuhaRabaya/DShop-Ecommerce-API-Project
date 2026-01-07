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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository , IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public async Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if (product == null) { 
              return new BaseResponse()
              {
                  Success = false,
                  Message="Product not found!"
              };
            }
            if (product.Quantity < request.Count)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "not enough product!"
                };
            }
            var cart = request.Adapt<Cart>();
            cart.UserId = userId;
            await _cartRepository.AddAsync(cart);

            return new BaseResponse()
            {
                Success = true,
                Message = "product added successfully"
            };
        }
    }
}
