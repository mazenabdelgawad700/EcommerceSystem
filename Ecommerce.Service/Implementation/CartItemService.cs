using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class CartItemService : ReturnBaseHandler, ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            this._cartItemRepository = cartItemRepository;
        }

        public async Task<ReturnBase<bool>> AddCartItemAsync(CartItem cartItem)
        {
            try
            {
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
    }
}
