using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model
{
    public class RefreshTokenCommand : IRequest<ReturnBase<string>>
    {
        public string AccessToken { get; set; } = null!;
    }
}
