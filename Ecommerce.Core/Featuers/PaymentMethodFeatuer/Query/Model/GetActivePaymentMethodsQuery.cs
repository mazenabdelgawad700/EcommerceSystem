using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Response;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Model
{
    public class GetActivePaymentMethodsQuery : IRequest<ReturnBase<IQueryable<GetPaymentMethodsResponse>>>
    {
        public GetActivePaymentMethodsQuery()
        {

        }
    }
}
