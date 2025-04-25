using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Cart> _dbSet;
        public CartRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Cart>();
        }
    }
}
