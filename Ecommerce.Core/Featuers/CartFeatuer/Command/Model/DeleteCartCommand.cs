using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CartFeatuer.Command.Model
{
    public class DeleteCartCommand : IRequest<ReturnBase<bool>>
    {
        public string UserId { get; set; }
        public int Id { get; set; }
    }
}
