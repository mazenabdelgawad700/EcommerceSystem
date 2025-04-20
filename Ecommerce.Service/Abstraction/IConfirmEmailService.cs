using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IConfirmEmailService
    {
        Task<ReturnBase<bool>> SendConfirmationEmailAsync(ApplicationUser user);
        Task<ReturnBase<bool>> ConfirmEmailAsync(string userId, string token);
    }
}
