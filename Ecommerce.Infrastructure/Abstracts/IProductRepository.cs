using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IBaseRepository;
using Ecommerce.Shared.Base;

namespace Ecommerce.Infrastructure.Abstracts
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<ReturnBase<bool>> DeleteProductAsync(Product product);
    }
}
