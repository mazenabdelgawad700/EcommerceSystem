using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface ICartItemService
    {
        Task<ReturnBase<bool>> AddCartItemAsync(CartItem cartItem);
    }
}
