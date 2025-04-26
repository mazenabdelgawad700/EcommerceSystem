using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IPaymentMethodService
    {
        Task<ReturnBase<bool>> AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task<ReturnBase<bool>> UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task<ReturnBase<bool>> DeletePaymentMethodAsync(int paymentMethodId);
        Task<ReturnBase<bool>> ActivatedPaymentMethodAsync(int paymentMethodId);
    }
}
