using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.RepositoriesBase;
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
            return services;
        }
    }
}