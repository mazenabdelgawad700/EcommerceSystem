using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IOrderItemService
    {
        Task<ReturnBase<bool>> AddOrderItem(OrderItem orderItem);
    }
}
