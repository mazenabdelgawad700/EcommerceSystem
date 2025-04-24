using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;

namespace Ecommerce.Infrastructure.RepositoriesBase
{
    internal class RecentSearchRepository : BaseRepository<RecentSearch>, IRecentSearchRepository
    {
        public RecentSearchRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
