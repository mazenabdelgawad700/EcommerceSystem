using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service.Implementation
{
    internal class PaymentMethodService : ReturnBaseHandler, IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            this._paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<ReturnBase<bool>> ActivatedPaymentMethodAsync(int paymentMethodId)
        {
            try
            {
                var paymentMethod = await _paymentMethodRepository.GetTableNoTracking().Data.Where(x => x.Id == paymentMethodId).FirstOrDefaultAsync();

                if (paymentMethod is null)
                    return BadRequest<bool>("payment method is not found");

                paymentMethod.IsActive = true;

                var updateResult = await _paymentMethodRepository.UpdateAsync(paymentMethod);
                return updateResult.Succeeded ? Success(true) : Failed<bool>(updateResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
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
        public async Task<ReturnBase<bool>> DeletePaymentMethodAsync(int paymentMethodId)
        {
            try
            {
                var paymentMethod = await _paymentMethodRepository.GetTableNoTracking().Data.Where(x => x.Id == paymentMethodId).FirstOrDefaultAsync();

                if (paymentMethod is null)
                    return BadRequest<bool>("payment method is not found");

                paymentMethod.IsActive = false;
                paymentMethod.UpdatedAt = DateTime.UtcNow;

                var updateResult = await _paymentMethodRepository.UpdateAsync(paymentMethod);
                return updateResult.Succeeded ? Success(true) : Failed<bool>(updateResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public ReturnBase<IQueryable<PaymentMethod>> GetActivePaymentMethods()
        {
            try
            {
                var paymentMethods = _paymentMethodRepository.GetTableNoTracking()
                    .Data.AsQueryable().Where(x => x.IsActive);

                return paymentMethods is not null ? Success(paymentMethods) : Failed<IQueryable<PaymentMethod>>("");
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<PaymentMethod>>(ex.InnerException.Message);
            }
        }
        public ReturnBase<IQueryable<PaymentMethod>> GetPaymentMethods(bool? active = null)
        {
            try
            {
                var paymentMethods = _paymentMethodRepository.GetTableNoTracking()
                    .Data.AsQueryable();

                if (active is not null)
                    paymentMethods = paymentMethods.Where(x => x.IsActive == active);

                return paymentMethods is not null ? Success(paymentMethods) : Failed<IQueryable<PaymentMethod>>("");
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<PaymentMethod>>(ex.InnerException.Message);
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
