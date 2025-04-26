using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
