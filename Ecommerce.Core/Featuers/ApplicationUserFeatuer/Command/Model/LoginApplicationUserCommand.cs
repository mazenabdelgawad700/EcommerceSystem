using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class LoginApplicationUserCommand : IRequest<ReturnBase<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
