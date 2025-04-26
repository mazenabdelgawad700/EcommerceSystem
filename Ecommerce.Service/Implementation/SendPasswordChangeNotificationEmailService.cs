using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class SendPasswordChangeNotificationEmailService : ReturnBaseHandler, ISendPasswordChangeNotificationEmailService
    {

        private readonly ISendEmailService _emailService;


        public SendPasswordChangeNotificationEmailService(ISendEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<ReturnBase<bool>> SendPasswordChangeNotificationAsync(ApplicationUser user)
        {
            try
            {
                if (user is not null)
                {
                    string message = @"
                <p>Your password has been successfully changed.</p>
                <p>If you did not request this change, please contact support immediately.</p>";

                    ReturnBase<bool> sendEmailResult = await _emailService.SendEmailAsync(
                        user.Email!,
                        message,
                        "Changing password",
                        "text/html"
                    );

                    return Success(sendEmailResult.Data == true, sendEmailResult.Message);
                }

                return Failed<bool>("User not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
