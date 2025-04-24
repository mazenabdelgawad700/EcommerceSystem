using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductService
    {
        Task<ReturnBase<int>> AddProductAsync(Product product, string userRole, string userId);
        Task<ReturnBase<int>> UpdateProductAsync(Product product);
        Task<ReturnBase<bool>> DeleteProductAsync(Product product);
        Task<ReturnBase<bool>> SaveProductImagesAsync(IEnumerable<IFormFile> files, int productId);
        Task<ReturnBase<bool>> UpdateProductImagesAsync(IEnumerable<IFormFile> files, IEnumerable<string> oldFiles, int productId);
        Task<ReturnBase<bool>> DeleteProductImagesAsync(int productId);
        Task<ReturnBase<Product>> GetProductByIdAsync(int productId);
        ReturnBase<IQueryable<Product>> GetProductsAsPaginated(int? categoryId = null, int? brandId = null);
        ReturnBase<IQueryable<Product>> SearchAboutProduct(string? searchQuery = null);
    }
}
