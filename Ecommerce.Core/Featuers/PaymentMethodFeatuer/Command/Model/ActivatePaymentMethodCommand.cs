using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Model
{
    public class ActivatePaymentMethodCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
    }
}
