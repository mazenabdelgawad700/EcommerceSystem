using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Command.Model
{
    public class DeleteProductCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
        public string SellerId { get; set; } = null!;
    }
}
