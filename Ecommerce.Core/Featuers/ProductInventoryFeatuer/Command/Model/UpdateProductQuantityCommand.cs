using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductInventoryFeatuer.Command.Model
{
    public class UpdateProductQuantityCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
