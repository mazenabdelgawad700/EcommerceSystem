using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IBaseRepository;
using Ecommerce.Shared.Base;

namespace Ecommerce.Infrastructure.Abstracts
{
    public interface IProductInventoryRepository : IBaseRepository<ProductInventory>
    {
        Task<ReturnBase<bool>> DeleteProductFromInventoryAsyncAsync(int productId);
    }
}
