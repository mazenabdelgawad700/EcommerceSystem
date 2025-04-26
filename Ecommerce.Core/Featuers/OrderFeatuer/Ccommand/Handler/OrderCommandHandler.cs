using AutoMapper;
using Ecommerce.Core.Featuers.OrderFeatuer.Ccommand.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.OrderFeatuer.Ccommand.Handler
{
    public class OrderCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddOrderCommand, ReturnBase<bool>>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderCommandHandler(IOrderService orderService, IMapper mapper)
        {
            this._orderService = orderService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var mappedResult = _mapper.Map<Order>(request);
                var addResult = await _orderService.AddOrderAsync(mappedResult);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
