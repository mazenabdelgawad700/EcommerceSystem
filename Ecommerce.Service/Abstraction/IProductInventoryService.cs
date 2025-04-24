using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductInventoryService
    {
        Task<ReturnBase<bool>> AddProductInventoryEntity(int productId, int inventoryId);
        Task<ReturnBase<bool>> DeleteProductFromInventory(int productId);
    }
}
