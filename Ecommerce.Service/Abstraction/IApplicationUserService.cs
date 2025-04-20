using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IApplicationUserService
    {
        Task<ReturnBase<bool>> RegisterApplicationUserAsync(ApplicationUser user, string password);
    }
}
