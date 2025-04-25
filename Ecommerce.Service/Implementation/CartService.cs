using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service.Implementation
{
    internal class CartService : ReturnBaseHandler, ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public async Task<ReturnBase<Cart>> GetCartAsync(string userId)
        {
            var cartResult = await _cartRepository.GetTableNoTracking().Data.Where(c => c.UserId == userId).FirstOrDefaultAsync();

            Cart cart;

            if (cartResult is null)
            {
                cart = new() { UserId = userId };
                var createCartResult = await CreateCartAsync(cart);
                if (!createCartResult.Succeeded)
                    return Failed<Cart>(createCartResult.Message);

                return Success(cart);
            }
            else
                cart = cartResult;

            var fullCart = await _cartRepository.GetTableNoTracking().Data
                                                .Include(c => c.CartItems)
                                                   .ThenInclude(i => i.Product)
                                                      .ThenInclude(p => p.ProductImages)
                                                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            return Success(fullCart!);
        }
        private async Task<ReturnBase<bool>> CreateCartAsync(Cart cart)
        {
            try
            {
                // Check if the user has cart or not

                var createCartResult = await _cartRepository.AddAsync(cart);
                if (!createCartResult.Succeeded)
                    return Failed<bool>(createCartResult.Message);

                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
