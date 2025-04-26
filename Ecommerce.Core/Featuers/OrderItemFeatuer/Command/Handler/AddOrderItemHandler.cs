using AutoMapper;
using Ecommerce.Core.Featuers.OrderItemFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.OrderItemFeatuer.Command.Handler
{
    public class AddOrderItemHandler : ReturnBaseHandler,
        IRequestHandler<AddOrderItemCommand, ReturnBase<bool>>
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;

        public AddOrderItemHandler(IOrderItemService orderItemService, IMapper mapper)
        {
            this._orderItemService = orderItemService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<OrderItem>(request);
                var addResult = await _orderItemService.AddOrderItem(mappedResult);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
