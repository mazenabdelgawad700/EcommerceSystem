using AutoMapper;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Handler
{
    public class PaymentMethodCommandHandler : ReturnBaseHandler, IRequestHandler<AddPaymentMethodCommand, ReturnBase<bool>>
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IMapper _mapper;

        public PaymentMethodCommandHandler(IPaymentMethodService paymentMethodService, IMapper mapper)
        {
            _paymentMethodService = paymentMethodService;
            _mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<PaymentMethod>(request);

                var addResult = await _paymentMethodService.AddPaymentMethodAsync(mappedResult);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
