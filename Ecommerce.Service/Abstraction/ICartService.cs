using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface ICartService
    {
        Task<ReturnBase<Cart>> GetCartAsync(string userId);
    }
}
