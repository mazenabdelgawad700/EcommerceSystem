using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductService
    {
        Task<ReturnBase<ulong>> AddProductAsync(Product product, string userRole, string userId);
        Task<ReturnBase<ulong>> UpdateProductAsync(Product product);
        Task<ReturnBase<bool>> SaveProductImagesAsync(IEnumerable<IFormFile> files, ulong productId);
        Task<ReturnBase<bool>> UpdateProductImagesAsync(IEnumerable<IFormFile> files, IEnumerable<string> oldFiles, ulong productId);
    }
}
