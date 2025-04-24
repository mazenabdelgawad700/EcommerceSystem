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

        public async Task<ReturnBase<bool>> AddProductInventoryEntity(int productId, int inventoryId)
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
        public async Task<ReturnBase<bool>> DeleteProductFromInventory(int productId)
        {
            try
            {
                var deleteResult = await _productInventoryRepository.DeleteProductFromInventoryAsync(productId);
                if (!deleteResult.Succeeded)
                    return Failed<bool>(deleteResult.Message);
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}