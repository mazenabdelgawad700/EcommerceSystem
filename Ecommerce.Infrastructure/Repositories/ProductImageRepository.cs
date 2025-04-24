using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;
using Ecommerce.Shared.Base;
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

        public async Task<bool> DeleteProductImage(string imgUrl, int productId)
        {
            try
            {
                var existingImage = await _dbSet.FirstOrDefaultAsync(p =>
                         p.ImageUrl == imgUrl && p.ProductId == productId);

                if (existingImage is null)
                    return false;

                _dbSet.Remove(existingImage);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ReturnBase<List<ProductImage>>> GetAllImagesForProduct(int productId)
        {
            try
            {
                //var images = await _dbContext.ProductImages
                //.FromSqlRaw("SELECT * FROM ProductImages WHERE ProductId = {0}", productId)
                //.ToListAsync(); 

                var images = _dbContext.ProductImages
                    .Where(x => x.ProductId == productId)
                    .ToList();

                return Success(images);
            }
            catch (Exception ex)
            {
                return Failed<List<ProductImage>>(ex.InnerException.Message);
            }
        }
        public async Task<bool> SaveProductImage(int productId, string imgUrl)
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
