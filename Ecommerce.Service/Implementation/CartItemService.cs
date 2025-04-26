using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class CartItemService : ReturnBaseHandler, ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductInventoryService _productInventoryService;

        public CartItemService(ICartItemRepository cartItemRepository, IProductInventoryService productInventoryService)
        {
            this._cartItemRepository = cartItemRepository;
            _productInventoryService = productInventoryService;
        }

        public async Task<ReturnBase<bool>> AddCartItemAsync(CartItem cartItem)
        {
            try
            {
                var existResult = _cartItemRepository.GetTableNoTracking().Data.Where(x => x.Id == cartItem.Id).FirstOrDefault();


                var checkInventoryResult = await _productInventoryService.CheckProductInInventoryAsync(cartItem.ProductId, cartItem.Quantity);

                if (!checkInventoryResult.Succeeded)
                    return BadRequest<bool>(checkInventoryResult.Message);

                if (existResult is not null)
                {
                    existResult.Quantity = cartItem.Quantity;

                    var updateQuantityResult = await _cartItemRepository.UpdateAsync(existResult);

                    if (!updateQuantityResult.Succeeded)
                        return Failed<bool>(updateQuantityResult.Message);
                }

                var addResult = await _cartItemRepository.AddAsync(cartItem);
                if (!addResult.Succeeded)
                    return Failed<bool>(addResult.Message);

                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> DeleteCartItemAsync(int cartItemId)
        {
            try
            {
                var deleteResult = await _cartItemRepository.DeleteAsync(cartItemId);
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
