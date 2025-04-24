using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IBaseRepository;
using Ecommerce.Shared.Base;

namespace Ecommerce.Infrastructure.Abstracts
{
    public interface IProductImageRepository : IBaseRepository<ProductImage>
    {
        Task<bool> SaveProductImage(int productId, string imgUrl);
        Task<bool> DeleteProductImage(string imgUrl, int productId);
        Task<ReturnBase<List<ProductImage>>> GetAllImagesForProduct(int productId);
    }
}
