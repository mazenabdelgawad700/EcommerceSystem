using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.OrderFeatuer.Ccommand.Model
{
    public class AddOrderCommand : IRequest<ReturnBase<int>>
    {
        public string UserId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
