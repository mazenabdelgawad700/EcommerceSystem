using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class ShippingAddressesRepository : BaseRepository<ShippingAddress>, IShippingAddressesRepository
    {
        public ShippingAddressesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
