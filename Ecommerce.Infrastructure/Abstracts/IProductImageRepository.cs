using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IBaseRepository;

namespace Ecommerce.Infrastructure.Abstracts
{
    public interface IProductImageRepository : IBaseRepository<ProductImage>
    {
        Task<bool> SaveProductImage(ulong productId, string imgUrl);
    }
}
