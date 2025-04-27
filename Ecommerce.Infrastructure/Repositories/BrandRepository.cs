using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
