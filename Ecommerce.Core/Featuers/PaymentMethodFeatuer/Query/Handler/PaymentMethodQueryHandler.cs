using AutoMapper;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Model;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Response;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Handler
{
    public class PaymentMethodQueryHandler : ReturnBaseHandler,
        IRequestHandler<GetPaymentMethodsQuery, ReturnBase<IQueryable<GetPaymentMethodsResponse>>>,
        IRequestHandler<GetActivePaymentMethodsQuery, ReturnBase<IQueryable<GetPaymentMethodsResponse>>>
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IMapper _mapper;

        public PaymentMethodQueryHandler(IPaymentMethodService paymentMethodService, IMapper mapper)
        {
            this._paymentMethodService = paymentMethodService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<IQueryable<GetPaymentMethodsResponse>>> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getMethodsResult = _paymentMethodService.GetPaymentMethods(request.Acitve ?? null);

                var mappedResult = _mapper.ProjectTo<GetPaymentMethodsResponse>(getMethodsResult.Data);

                return getMethodsResult.Succeeded ? Success(mappedResult) : Failed<IQueryable<GetPaymentMethodsResponse>>(getMethodsResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<GetPaymentMethodsResponse>>(ex.InnerException.Message);
            }
        }

        public async Task<ReturnBase<IQueryable<GetPaymentMethodsResponse>>> Handle(GetActivePaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getActiveMethodsResult = _paymentMethodService.GetActivePaymentMethods();

                var mappedResult = _mapper.ProjectTo<GetPaymentMethodsResponse>(getActiveMethodsResult.Data);

                return getActiveMethodsResult.Succeeded ? Success(mappedResult) : Failed<IQueryable<GetPaymentMethodsResponse>>(getActiveMethodsResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<GetPaymentMethodsResponse>>(ex.InnerException.Message);
            }
        }
    }
}
