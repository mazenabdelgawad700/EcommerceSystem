using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IBrandService
    {
        Task<ReturnBase<bool>> AddBrandAsync(Brand brand);
        Task<ReturnBase<bool>> UpdateBrandAsync(Brand brand);
        Task<ReturnBase<bool>> DeleteBrandAsync(int brandId);
        ReturnBase<IQueryable<Product>> GetBrandProducts(int brandId);
    }
}
