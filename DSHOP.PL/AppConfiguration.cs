using DSHOP.BLL.Service;
using DSHOP.DAL.Repository;
using DSHOP.DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace DSHOP.PL
{
    public static class AppConfiguration
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IFileService, FileService>(); 

            services.AddScoped<ISeedData, RoleSeedData>();
            services.AddScoped<ISeedData, UserSeedData>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<ICartService, CartService>();
            services.AddTransient<ICartRepository, CartRepository>();

            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICheckoutService, CheckoutService>();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
