using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

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
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> CheckProductInInventoryAsync(int productId, int quantity)
        {
            try
            {
                var product = await _productInventoryRepository.GetTableNoTracking()
                    .Data.Where(x => x.ProductId == productId).FirstOrDefaultAsync();

                if (product is null)
                    return Failed<bool>("Failed to get product");

                return product.Quantity >= quantity ? Success(true) : Failed<bool>("Inventory Limit exceeded");
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> DeleteProductFromInventoryAsync(int productId)
        {
            try
            {
                var deleteResult = await _productInventoryRepository.DeleteProductFromInventoryAsyncAsync(productId);
                if (!deleteResult.Succeeded)
                    return Failed<bool>(deleteResult.Message);
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> UpdateProductQuantityAsync(ProductInventory productInventory)
        {
            try
            {
                var updateResult = await _productInventoryRepository.UpdateAsync(productInventory);
                if (!updateResult.Succeeded)
                    return Failed<bool>(updateResult.Message);
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}