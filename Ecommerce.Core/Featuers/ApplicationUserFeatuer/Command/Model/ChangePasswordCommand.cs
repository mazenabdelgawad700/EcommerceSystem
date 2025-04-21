using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class ChangePasswordCommand : IRequest<ReturnBase<bool>>
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
