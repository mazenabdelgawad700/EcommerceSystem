using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IBaseRepository;
using Ecommerce.Shared.Base;

namespace Ecommerce.Infrastructure.Abstracts
{
    public interface IApplicationUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<ReturnBase<ApplicationUser>> GetApplicationUserByIdAsync(string userId);
    }
}
