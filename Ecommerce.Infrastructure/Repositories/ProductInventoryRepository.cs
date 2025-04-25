using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class ProductInventoryRepository : BaseRepository<ProductInventory>, IProductInventoryRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<ProductInventory> _dbSet;
        public ProductInventoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProductInventory>();
        }

        public async Task<ReturnBase<bool>> DeleteProductFromInventoryAsyncAsync(int productId)
        {
            try
            {
                var products = await GetTableNoTracking().Data.Where(x => x.ProductId == productId).ToListAsync();
                var deleteResult = await DeleteRangeAsync(products);
                if (!deleteResult.Succeeded)
                    return Failed<bool>(deleteResult.Message);
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}