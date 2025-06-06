﻿using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<ReturnBase<bool>> AddCategoryAsync(Category category);
        Task<ReturnBase<bool>> UpdateCategoryAsync(Category category);
        Task<ReturnBase<bool>> DeleteCategoryAsync(int categoryId);
        ReturnBase<IQueryable<Product>> GetCategoryProductsAsync(int categoryId);
    }
}
