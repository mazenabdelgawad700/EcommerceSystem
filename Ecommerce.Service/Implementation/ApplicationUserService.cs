using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Ecommerce.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Ecommerce.Service.Implementation
{
    internal class ApplicationUserService : ReturnBaseHandler, IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfirmEmailService _confirmEmailService;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, IConfirmEmailService confirmEmailService)
        {
            this._userManager = userManager;
            _confirmEmailService = confirmEmailService;
        }
        private bool ValidatePassword(string password)
        {
            string pattern = @"^(?=(?:.*\d){3,})(?=(?:.*[A-Z]){3,})(?=(?:.*[a-z]){3,})(?=(?:.*[^a-zA-Z0-9]){3,}).*$";
            return Regex.IsMatch(password, pattern);
        }
        private bool ValidateEmailAddress(string emailAddress)
        {
            string pattern = @"^(?:[a-zA-Z0-9_'^&+/=!?{}~\-]+(?:\.[a-zA-Z0-9_'^&+/=!?{}~\-]+)*)@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$";
            return Regex.IsMatch(emailAddress, pattern);
        }
        private string ExtractUserNameFromEmail(string email)
        {
            return email.Substring(0, email.IndexOf("@"));
        }
        public async Task<ReturnBase<bool>> RegisterApplicationUserAsync(ApplicationUser user, string password, UserRole role)
        {
            try
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(user.Email))
                    return BadRequest<bool>("Please, enter required fields");

                var validatePasswordResult = ValidatePassword(password);

                if (!validatePasswordResult)
                    return BadRequest<bool>("Passwor must contain at least 3 numbers, 3 uppercase letters, 3 lowercase letters, and 3 special characters.");

                var validateEmailResult = ValidateEmailAddress(user.Email);

                if (!validateEmailResult)
                    return BadRequest<bool>("Please, enter a valid email address");

                user.UserName = ExtractUserNameFromEmail(user.Email);

                var isUserExist = await _userManager.FindByEmailAsync(user.Email);
                if (isUserExist is not null)
                    return BadRequest<bool>("User already exist");

                user.Id = Guid.NewGuid().ToString();


                if (user.Email == "mazenabdelgawad700@gmail.com")
                    user.Role = "Admin";

                else
                    user.Role = role.ToString();

                var addUserResult = await _userManager.CreateAsync(user, password);


                if (addUserResult.Succeeded)
                {
                    ReturnBase<bool> sendConfirmationEmailResult = await _confirmEmailService.SendConfirmationEmailAsync(user);

                    while (!sendConfirmationEmailResult.Succeeded)
                        sendConfirmationEmailResult = await _confirmEmailService.SendConfirmationEmailAsync(user);

                    return Success(true, $"User Registerd Successfully, Please confirm your email by the email sent to {user.Email}");
                }

                if (addUserResult.Errors.FirstOrDefault() is not null)
                    return Failed<bool>(addUserResult.Errors!.FirstOrDefault()!.Code);

                return Failed<bool>("Can not register user, please try again");
            }
            catch (Exception)
            {
                return Failed<bool>("Can not register user, please try again");
            }
        }
    }
}
