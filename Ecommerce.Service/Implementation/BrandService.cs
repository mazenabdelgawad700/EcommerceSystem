using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class BrandService : ReturnBaseHandler, IBrandService
    {
        private readonly IBrandRepository _brandRepositorIy;

        public BrandService(IBrandRepository brandRepository)
        {
            this._brandRepositorIy = brandRepository;
        }

        public async Task<ReturnBase<bool>> AddBrandAsync(Brand brand)
        {
            try
            {
                var addBrandResult = await _brandRepositorIy.AddAsync(brand);
                return addBrandResult.Succeeded ? Success(true) : Failed<bool>(addBrandResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> UpdateBrandAsync(Brand brand)
        {
            try
            {
                var updateBrandResult = await _brandRepositorIy.UpdateAsync(brand);
                return updateBrandResult.Succeeded ? Success(true) : Failed<bool>(updateBrandResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
