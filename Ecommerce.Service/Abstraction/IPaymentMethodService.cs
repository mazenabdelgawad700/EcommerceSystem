using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IPaymentMethodService
    {
        Task<ReturnBase<bool>> AddPaymentMethodAsync(PaymentMethod paymentMethod);
    }
}
