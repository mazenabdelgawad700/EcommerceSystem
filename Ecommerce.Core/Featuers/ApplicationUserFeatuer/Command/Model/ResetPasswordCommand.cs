using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class ResetPasswordCommand : IRequest<ReturnBase<bool>>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}
