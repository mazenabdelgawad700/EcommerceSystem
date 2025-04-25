using Ecommerce.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ecommerce.Service.Implementation
{
    public class CartCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromDays(1);

        public CartCleanupService(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanupCartsAsync();
                }
                catch { }

                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task CleanupCartsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var cutoffDate = DateTime.UtcNow.AddMonths(-1);

            var oldCarts = dbContext.Carts
                .Where(c => c.SessionId < cutoffDate)
                .ToList();

            if (oldCarts.Count != 0)
            {
                dbContext.Carts.RemoveRange(oldCarts);
            }

            var cartsWithInvalidProducts = dbContext.Carts
                .Where(c => c.CartItems.Any(i => !dbContext.Products.Any(p => p.Id == i.ProductId)))
                .ToList();

            if (cartsWithInvalidProducts.Count != 0)
            {
                dbContext.Carts.RemoveRange(cartsWithInvalidProducts);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
