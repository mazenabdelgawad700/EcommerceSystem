using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class ProductInventoryRepository : BaseRepository<ProductInventory>, IProductInventoryRepository
    {
        public ProductInventoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}