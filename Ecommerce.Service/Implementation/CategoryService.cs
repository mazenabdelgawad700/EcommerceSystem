using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class CategoryService : ReturnBaseHandler, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
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
