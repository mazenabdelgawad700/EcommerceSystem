using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductService
    {
        Task<ReturnBase<ulong>> AddProductAsync(Product product, string userRole, string userId);
        Task<ReturnBase<bool>> SaveProductImagesAsync(IEnumerable<IFormFile> files, ulong productId);
    }
}
