using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Model
{
    public class UpdatePaymentMethodCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
