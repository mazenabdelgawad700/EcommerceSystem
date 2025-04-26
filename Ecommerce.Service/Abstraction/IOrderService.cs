using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IOrderService
    {
        Task<ReturnBase<bool>> AddOrderAsync(Order order);
    }
}
