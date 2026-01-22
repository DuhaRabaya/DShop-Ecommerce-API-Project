using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using DSHOP.DAL.Repository;
using Mapster;

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
            var item=await _cartRepository.getItem(userId, request.ProductId);

            if (item is not null) {
                if (product.Quantity<(item.Count + request.Count)) {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "not enough product!"
                    };
                }
                item.Count += request.Count;
                await _cartRepository.updateAsync(item);
            }
            else
            {
                var cart = request.Adapt<Cart>();
                cart.UserId = userId;
                await _cartRepository.AddAsync(cart);
            }
            return new BaseResponse()
            {
                Success = true,
                Message = "product added successfully"
            };
        }

        public async Task<BaseResponse> RemoveFromCart(string userId, int productId)
        {
            var product = await _cartRepository.getItem(userId, productId);
            if (product is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "product not found"
                };
            }

            await _cartRepository.DeleteAsync(product);
            return new BaseResponse()
            {
                Success = true,
                Message = "item deleted successfully"
            };
        }
        public async Task<CartSummaryResponse> getItems(string userId, string lang = "en")
        {
            var items=await _cartRepository.getItems(userId);

            var response = items.Select(c => new CartResponse
            {
                ProductId = c.ProductId,
                ProductName = c.Product.Translations.FirstOrDefault(t=>t.Language==lang).Name,
                Count = c.Count,
                Price=c.Product.Price
            }).ToList();

            return new CartSummaryResponse
            {
                Items = response,

            };

        }
        public async Task<BaseResponse> UpdateQuantity(string userId,int productId, int quantity)
        {
            var item = await _cartRepository.getItem(userId, productId);
            var product =await _productRepository.FindByIdAsync(productId);

            if (quantity == 0)
            {
                await _cartRepository.DeleteAsync(item);
                return new BaseResponse()
                {
                    Success = true,
                    Message = "item removed"
                };
            }else if (quantity < 0)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "invalid quantity"
                };
            }
            if (product.Quantity < quantity)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "not enough stock"
                };
            }

            item.Count=quantity;
            await _cartRepository.updateAsync(item);
            return new BaseResponse()
            {
                Success = true,
                Message = "quantity updated successfully"
            };
        }

        public async Task<BaseResponse> clearCart(string userId)
        {
            await _cartRepository.clearCart(userId);

            return new BaseResponse()
            {
                Success = true,
                Message = "Cart cleared"
            };
        }
    }
}
