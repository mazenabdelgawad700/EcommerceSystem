using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;
using Ecommerce.Shared.Enums;

namespace Ecommerce.Service.Abstraction
{
    public interface IApplicationUserService
    {
        Task<ReturnBase<bool>> RegisterApplicationUserAsync(ApplicationUser user, string password, UserRole role);
        Task<ReturnBase<string>> LoginAsync(string email, string password);
        Task<ReturnBase<string>> RefreshTokenAsync(string accessToken);
    }
}
