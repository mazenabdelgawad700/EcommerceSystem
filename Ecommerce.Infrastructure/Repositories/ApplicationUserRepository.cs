using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Infrastructure.RepositoriesBase;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<ApplicationUser> _dbSet;
        public ApplicationUserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ApplicationUser>();
        }

        public async Task<ReturnBase<ApplicationUser>> GetApplicationUserByIdAsync(string userId)
        {
            try
            {
                var user = await _dbSet.Where(x => x.Id == userId)
                                       .Include(x => x.Orders)
                                       .FirstOrDefaultAsync();

                if (user is null)
                    return Failed<ApplicationUser>("Invalid user id");

                return Success(user);
            }
            catch (Exception ex)
            {
                return Failed<ApplicationUser>(ex.InnerException.Message);
            }
        }
    }
}
