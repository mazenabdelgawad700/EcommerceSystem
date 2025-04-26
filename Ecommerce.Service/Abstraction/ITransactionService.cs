using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface ITransactionService
    {
        Task<ReturnBase<bool>> AddTransactionAsync(Transaction transaction);
    }
}
