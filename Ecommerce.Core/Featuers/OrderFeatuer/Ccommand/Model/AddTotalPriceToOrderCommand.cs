using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.OrderFeatuer.Ccommand.Model
{
    public class AddTotalPriceToOrderCommand : IRequest<ReturnBase<bool>>
    {
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}
