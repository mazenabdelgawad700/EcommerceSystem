using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<ProductImage> _dbSet;
        public ProductImageRepository(AppDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<ProductImage>();
        }

        public async Task<bool> SaveProductImage(ulong productId, string imgUrl)
        {
            try
            {
                var entity = new ProductImage
                {
                    ImageUrl = imgUrl,
                    ProductId = productId
                };
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
