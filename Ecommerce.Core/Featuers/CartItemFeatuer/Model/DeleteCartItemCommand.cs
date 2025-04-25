using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CartItemFeatuer.Model
{
    public class DeleteCartItemCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
    }
}
