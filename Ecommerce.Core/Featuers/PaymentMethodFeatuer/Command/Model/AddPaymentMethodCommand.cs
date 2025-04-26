using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Model
{
    public class AddPaymentMethodCommand : IRequest<ReturnBase<bool>>
    {
        public string PaymentMethodName { get; set; }
    }
}
