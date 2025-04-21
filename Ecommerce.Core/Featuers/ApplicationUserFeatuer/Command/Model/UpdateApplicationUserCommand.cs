using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class UpdateApplicationUserCommand : IRequest<ReturnBase<bool>>
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
    }
}
