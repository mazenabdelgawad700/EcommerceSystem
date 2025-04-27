using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class CategoryService : ReturnBaseHandler, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            this._categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<ReturnBase<bool>> AddCategoryAsync(Category category)
        {
            try
            {
                var addResult = await _categoryRepository.AddAsync(category);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var deleteResult = await _categoryRepository.DeleteAsync(categoryId);
                return deleteResult.Succeeded ? Success(true) : Failed<bool>(deleteResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public ReturnBase<IQueryable<Product>> GetCategoryProductsAsync(int categoryId)
        {
            try
            {
                var products = _productRepository.GetTableNoTracking().Data.Where(x => x.CategoryId == categoryId).AsQueryable();

                return products is not null ? Success(products) : Failed<IQueryable<Product>>("Can not get products, please try again");
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<Product>>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> UpdateCategoryAsync(Category category)
        {
            try
            {
                var updateResult = await _categoryRepository.UpdateAsync(category);
                return updateResult.Succeeded ? Success(true) : Failed<bool>(updateResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
