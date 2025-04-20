using Ecommerce.Shared.Base;
using Ecommerce.Shared.Enums;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class RegisterApplicationUserCommand : IRequest<ReturnBase<bool>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
