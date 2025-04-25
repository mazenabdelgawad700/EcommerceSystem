using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CartItemFeatuer.Model
{
    public class AddCartItemCommand : IRequest<ReturnBase<bool>>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
