using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service.Implementation
{
    internal class OrderService : ReturnBaseHandler, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public async Task<ReturnBase<int>> AddOrderAsync(Order order)
        {
            try
            {
                order.PlacedAt = DateTime.UtcNow;
                var addResult = await _orderRepository.AddAsync(order);
                return addResult.Succeeded ? Success(order.OrderId) : Failed<int>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<int>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> AddTotalPriceToOrderAsync(int orderId, decimal totalPrice)
        {
            try
            {
                var order = await _orderRepository.GetTableNoTracking().Data.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

                if (order is null)
                    return Failed<bool>("Can not get order");

                order.TotalPrice = totalPrice;
                var updateResult = await _orderRepository.UpdateAsync(order);

                return updateResult.Succeeded ? Success(true) : Failed<bool>(updateResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
