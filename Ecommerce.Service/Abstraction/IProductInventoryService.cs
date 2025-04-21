using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductInventoryService
    {
        Task<ReturnBase<bool>> AddProductInventoryEntity(ulong productId, int inventoryId);
    }
}
