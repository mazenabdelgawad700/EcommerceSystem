using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class OrderItemService : ReturnBaseHandler, IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            this._orderItemRepository = orderItemRepository;
        }

        public async Task<ReturnBase<bool>> AddOrderItem(OrderItem orderItem)
        {
            try
            {
                var addResult = await _orderItemRepository.AddAsync(orderItem);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
