using DSHOP.BLL.Service;
using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.Repository;
using DSHOP.DAL.Utils;
using DSHOP.DAL.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace DSHOP.PL
{
    public static class AppConfiguration
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, BLL.Service.ProductService>();

            services.AddScoped<IFileService, BLL.Service.FileService>(); 

            services.AddScoped<ISeedData, RoleSeedData>();
            services.AddScoped<ISeedData, UserSeedData>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<ICartService, CartService>();
            services.AddTransient<ICartRepository, CartRepository>();

            services.AddScoped<IManageUserService, ManageUserService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, BLL.Service.ReviewService>();


            services.AddScoped<ITokenService,BLL.Service.TokenService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICheckoutService, BLL.Service.CheckoutService>();

           
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ProductValidation>();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
