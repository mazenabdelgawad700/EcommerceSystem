using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class ConfirmEmailCommand : IRequest<ReturnBase<bool>>
    {
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
