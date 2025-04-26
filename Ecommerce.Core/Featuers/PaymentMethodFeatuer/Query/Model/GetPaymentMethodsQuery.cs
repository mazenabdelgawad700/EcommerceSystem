using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Response;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Model
{
    public class GetPaymentMethodsQuery : IRequest<ReturnBase<IQueryable<GetPaymentMethodsResponse>>>
    {
        public bool? Acitve { get; set; } = null;
    }
}
