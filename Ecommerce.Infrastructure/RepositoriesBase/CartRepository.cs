using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.RepositoriesBase
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
