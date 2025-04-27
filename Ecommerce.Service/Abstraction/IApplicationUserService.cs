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
        Task<ReturnBase<bool>> SendResetPasswordEmailAsync(string email);
        Task<ReturnBase<bool>> ResetPasswordAsync(string resetPasswordToken, string newPassword, string email);
        Task<ReturnBase<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<ReturnBase<ApplicationUser>> GetApplicationUserByIdAsync(string userId);
        Task<ReturnBase<bool>> UpdateApplicationUserAsync(string userId, string newEmail);
    }
}
