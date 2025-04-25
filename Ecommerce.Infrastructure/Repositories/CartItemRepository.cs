using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
