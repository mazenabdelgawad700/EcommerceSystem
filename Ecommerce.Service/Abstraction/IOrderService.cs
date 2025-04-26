using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IOrderService
    {
        Task<ReturnBase<int>> AddOrderAsync(Order order);
        Task<ReturnBase<bool>> AddTotalPriceToOrderAsync(int orderId, decimal totalPrice);
    }
}
