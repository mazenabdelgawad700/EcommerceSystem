using Ecommerce.Service.Abstraction;
using Ecommerce.Service.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class ModuleIServiceDependancies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<ISendEmailService, SendEmailService>();
            services.AddTransient<IConfirmEmailService, ConfirmEmailService>();
            services.AddTransient<ISendPasswordChangeNotificationEmailService, SendPasswordChangeNotificationEmailService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IProductInventoryService, ProductInventoryService>();
            services.AddTransient<ICartService, CartService>();
            return services;
        }
    }
}