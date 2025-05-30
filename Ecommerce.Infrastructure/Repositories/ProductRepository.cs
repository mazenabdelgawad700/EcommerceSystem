﻿using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Product>();
        }

        public async Task<ReturnBase<bool>> DeleteProductAsync(Product product)
        {
            try
            {
                _dbSet.Remove(product);
                await _dbContext.SaveChangesAsync();
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<Product>> GetProductAsync(int id)
        {
            try
            {
                var product = await _dbSet.Where(x => x.Id == id)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync();


                if (product == null)
                    return Failed<Product>("Can not get product");
                return Success(product);
            }
            catch (Exception ex)
            {
                return Failed<Product>(ex.InnerException.Message);
            }
        }
    }
}
