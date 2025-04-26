using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.OrderItemFeatuer.Command.Model
{
    public class AddOrderItemCommand : IRequest<ReturnBase<bool>>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public decimal PiecePrice { get; set; }
    }
}
