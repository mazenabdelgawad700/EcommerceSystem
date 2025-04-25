using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CartFeatuer.Commnad.Model
{
    public class CreateCartCommand : IRequest<ReturnBase<bool>>
    {
        public string UserId { get; set; }
    }
}
