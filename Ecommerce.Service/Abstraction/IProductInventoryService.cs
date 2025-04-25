using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductInventoryService
    {
        Task<ReturnBase<bool>> AddProductInventoryEntity(int productId, int inventoryId);
        Task<ReturnBase<bool>> CheckProductInInventoryAsync(int productId, int inventoryId);
        Task<ReturnBase<bool>> DeleteProductFromInventoryAsync(int productId);
        Task<ReturnBase<bool>> UpdateProductQuantityAsync(ProductInventory productInventory);
    }
}
