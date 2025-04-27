using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service.Implementation
{
    internal class BrandService : ReturnBaseHandler, IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IProductRepository _productRepository;

        public BrandService(IBrandRepository brandRepository, IProductRepository productRepository)
        {
            this._brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        public async Task<ReturnBase<bool>> AddBrandAsync(Brand brand)
        {
            try
            {
                var addBrandResult = await _brandRepository.AddAsync(brand);
                return addBrandResult.Succeeded ? Success(true) : Failed<bool>(addBrandResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> DeleteBrandAsync(int brandId)
        {
            try
            {
                var products = await _productRepository.GetTableNoTracking().Data.Where(x => x.BrandId == brandId).ToListAsync();

                if (products.Count > 0)
                    return Failed<bool>("you can not delete brand because there are products associated with it");

                var updateBrandResult = await _brandRepository.DeleteAsync(brandId);
                return updateBrandResult.Succeeded ? Success(true) : Failed<bool>(updateBrandResult.Message);
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
                var updateBrandResult = await _brandRepository.UpdateAsync(brand);
                return updateBrandResult.Succeeded ? Success(true) : Failed<bool>(updateBrandResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
