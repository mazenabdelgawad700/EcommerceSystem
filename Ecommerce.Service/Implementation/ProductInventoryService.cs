using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class ProductInventoryService : ReturnBaseHandler, IProductInventoryService
    {
        private readonly IProductInventoryRepository _productInventoryRepository;


        public ProductInventoryService(IProductInventoryRepository productInventoryRepository)
        {
            this._productInventoryRepository = productInventoryRepository;
        }

        public async Task<ReturnBase<bool>> AddProductInventoryEntity(ulong productId, int inventoryId)
        {
            try
            {
                var entity = new ProductInventory()
                {
                    ProductId = productId,
                    InventoryId = inventoryId
                };
                await _productInventoryRepository.AddAsync(entity);
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
    }
}