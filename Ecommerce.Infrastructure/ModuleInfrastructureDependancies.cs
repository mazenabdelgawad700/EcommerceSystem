using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class ModuleInfrastructureDependancies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IProductInventoryRepository, ProductInventoryRepository>();
            services.AddTransient<IRecentSearchRepository, RecentSearchRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IShippingAddressesRepository, ShippingAddressesRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            return services;
        }
    }
}