using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using DSHOP.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public CheckoutService(ICartRepository cartRepository , IOrderRepository orderRepository ,
            UserManager<ApplicationUser> userManager , IEmailSender emailSender)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _emailSender = emailSender;
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
                if (item.Product.Quantity < item.Count)
                {
                    return new CheckoutResponse
                    {
                        Success = false,
                        Message = "not enough product!"
                    };
                }
                total += item.Product.Price * item.Count;
            }
            Order order = new Order
            {
                UserId = UserId,
                PaymentMethod = request.PaymentMethod,
                AmountPaid =total,

             };


            if (request.PaymentMethod == PaymentMethodEnum.Cash) {
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Pay in cash!"
                };
            }
            else if (request.PaymentMethod == PaymentMethodEnum.Visa) {

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },

                    Mode = "payment",
                    SuccessUrl = $"http://localhost:5051/api/checkout/success?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"http://localhost:5051/checkout/cancel",
                    LineItems = new List<SessionLineItemOptions>(),
                    Metadata = new Dictionary<string, string>
                    {
                        {"UserId" , UserId},
                    }
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
                            UnitAmount = (long)item.Product.Price*100,
                        },
                        Quantity = item.Count,
                    });
                }

                
                var service = new SessionService();
                var session = service.Create(options);
                order.SessionId = session.Id;
                await _orderRepository.CreateAsync(order);

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

        public async Task<CheckoutResponse> HandleSuccess(string sessionId)
        {
            var service = new SessionService();
            var session = service.Get(sessionId);
            var userId = session.Metadata["UserId"];
            var order= await _orderRepository.GetBySessionId(sessionId);
            
            order.PaymentId = session.PaymentIntentId;
            order.OrderStatus = OrderStatusEnum.Approved;
            await _orderRepository.UpdateAsync(order);

            var user =await  _userManager.FindByIdAsync(userId);
            await _emailSender.SendEmailAsync(user.Email, "Successfull payment", "<h1>payment process completed successfully</h1>");

            return new CheckoutResponse
            {
                Success = true,
                Message = "successfull payment"
            };
        }


    }
}
