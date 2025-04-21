using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    public interface ISendPasswordChangeNotificationEmailService
    {
        Task<ReturnBase<bool>> SendPasswordChangeNotificationAsync(ApplicationUser user);
    }
}
