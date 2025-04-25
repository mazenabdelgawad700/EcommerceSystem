using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class RecentSearchRepository : BaseRepository<RecentSearch>, IRecentSearchRepository
    {
        public RecentSearchRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
