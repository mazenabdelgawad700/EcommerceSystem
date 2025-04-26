using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class PaymentMethodService : ReturnBaseHandler, IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            this._paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<ReturnBase<bool>> AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            try
            {
                if (string.IsNullOrEmpty(paymentMethod.PaymentMethodName))
                {
                    return Failed<bool>("Payment method name is required");
                }
                var addResult = await _paymentMethodRepository.AddAsync(paymentMethod);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            try
            {
                if (string.IsNullOrEmpty(paymentMethod.PaymentMethodName))
                {
                    return Failed<bool>("Payment method name is required");
                }
                paymentMethod.UpdatedAt = DateTime.UtcNow;
                var updateResult = await _paymentMethodRepository.UpdateAsync(paymentMethod);
                return updateResult.Succeeded ? Success(true) : Failed<bool>(updateResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
