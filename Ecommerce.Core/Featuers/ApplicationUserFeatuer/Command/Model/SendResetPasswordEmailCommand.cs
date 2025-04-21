using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class SendResetPasswordEmailCommand : IRequest<ReturnBase<bool>>
    {
        public string Email { get; set; } = null!;
    }
}
