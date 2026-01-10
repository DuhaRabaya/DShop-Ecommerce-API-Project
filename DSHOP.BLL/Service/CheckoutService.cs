using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Repository;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;

        public CheckoutService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CheckoutResponse> PaymentProcess(CheckoutRequest request, string UserId)
        {
            var cartItems = await _cartRepository.getItems(UserId);
            if (!cartItems.Any()) {
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "cart is empty!"
                };    
            }

            decimal total = 0;
            foreach (var item in cartItems)
            {
                if(item.Product.Quantity< item.Count)
                {
                    return new CheckoutResponse
                    {
                        Success = false,
                        Message = "not enough product!"
                    };
                }
                total += item.Product.Price*item.Count;
            }

            if(request.PaymentMethod == "cash") {
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Pay in cash!"
                };
            }
            else if (request.PaymentMethod == "visa") {

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },

                    Mode = "payment",
                    SuccessUrl = $"http://localhost:5051/checkout/success",
                    CancelUrl = $"http://localhost:5051/checkout/cancel",
                    LineItems = new List<SessionLineItemOptions>()
                };

                foreach(var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Translations.FirstOrDefault(t => t.Language == "en").Name,
                            },
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }

                var service = new SessionService();
                var session = service.Create(options);

                return new CheckoutResponse
                {
                    Success = true,
                    Message = "payment session created",
                    Url=session.Url,
                    PaymentId=session.PaymentIntentId
                    
                };
            }
            else
            {
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "invalid payment method!"
                };
            }
        }
    }
}
