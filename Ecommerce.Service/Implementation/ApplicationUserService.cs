using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Helpers;
using Ecommerce.Infrastructure.Context;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Ecommerce.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Ecommerce.Service.Implementation
{
    internal class ApplicationUserService : ReturnBaseHandler, IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfirmEmailService _confirmEmailService;
        private readonly AppDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISendEmailService _emailService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISendPasswordChangeNotificationEmailService
           _sendPasswordChangeNotificationEmailService;


        public ApplicationUserService(UserManager<ApplicationUser> userManager, IConfirmEmailService confirmEmailService, JwtSettings jwtSettings, AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, ISendEmailService emailService, SignInManager<ApplicationUser> signInManager, ISendPasswordChangeNotificationEmailService
           sendPasswordChangeNotificationEmailService)
        {
            this._userManager = userManager;
            this._confirmEmailService = confirmEmailService;
            this._jwtSettings = jwtSettings;
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;
            this._emailService = emailService;
            this._signInManager = signInManager;
            this._sendPasswordChangeNotificationEmailService = sendPasswordChangeNotificationEmailService;
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

                await _userManager.AddToRoleAsync(user, role.ToString());

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
        public async Task<ReturnBase<string>> LoginAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return Failed<string>("Invalid Credentials");

                ApplicationUser? user = await _userManager.FindByEmailAsync(email);
                if (user is null)
                    return Failed<string>("Wrong Email Or Password");

                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);

                if (!isPasswordCorrect)
                    return Failed<string>("Wrong Email Or Password");

                string jwtId = Guid.NewGuid().ToString();
                string token = await GenerateJwtToken(user, jwtId);

                await BuildRefreshToken(user, jwtId);

                if (!user.EmailConfirmed)
                {
                    ReturnBase<bool> sendConfirmationEmailResult = await _confirmEmailService.SendConfirmationEmailAsync(user);
                    if (sendConfirmationEmailResult.Succeeded)
                    {
                        return Success<string>($"A Confirmation Email has been sent to {user.Email}. Please confirm your email first and then log in.");
                    }
                }
                return Success(token, "Logged in successfully");

            }
            catch (Exception ex)
            {
                return Failed<string>(ex.Message);
            }
        }
        private async Task<string> GenerateJwtToken(ApplicationUser user, string jwtId)
        {
            List<Claim> claims = await GetClaimsAsync(user, jwtId);

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user, string jwtId)
        {
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims =
            [
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jwtId),
            ];
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        private async Task BuildRefreshToken(ApplicationUser user, string jwtId)
        {
            RefreshToken newRefreshToken = new()
            {
                UserId = user.Id,
                UserRefreshToken = GenerateRefreshToken(),
                JwtId = jwtId,
                IsUsed = false,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMonths(_jwtSettings.RefreshTokenExpireDate)
            };

            RefreshToken? existingRefreshTokenRecord = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == user.Id);

            if (existingRefreshTokenRecord is null)
            {
                await _dbContext.RefreshTokens.AddAsync(newRefreshToken);
            }
            else
            {
                existingRefreshTokenRecord.UserRefreshToken = GenerateRefreshToken();
                existingRefreshTokenRecord.CreatedAt = DateTime.UtcNow;
                existingRefreshTokenRecord.ExpiresAt = DateTime.UtcNow.AddMonths(_jwtSettings.RefreshTokenExpireDate);

                _dbContext.RefreshTokens.Update(existingRefreshTokenRecord);
            }

            await _dbContext.SaveChangesAsync();
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private bool IsAccessTokenExpired(string accessToken)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();
                if (tokenHandler.ReadToken(accessToken) is not JwtSecurityToken token)
                    return true;

                DateTimeOffset expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp).Value));

                return expirationTime.UtcDateTime <= DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }
        public async Task<ReturnBase<string>> RefreshTokenAsync(string accessToken)
        {
            try
            {
                if (!IsAccessTokenExpired(accessToken))
                    return Success("", "Access Token Is Valid");

                string? userId = GetUserIdFromToken(accessToken);
                string? jwtId = GetJwtIdFromToken(accessToken);

                if (jwtId is null || userId is null)
                    return Failed<string>("Invalid Access Token");

                RefreshToken? storedRefreshToken = await _dbContext.RefreshTokens
                    .FirstOrDefaultAsync(rt => rt.UserId.ToString() == userId && rt.JwtId == jwtId);

                if (storedRefreshToken is null || storedRefreshToken.IsRevoked)
                    return Failed<string>("Your session has expired. please log in again.");

                if (storedRefreshToken.ExpiresAt < DateTime.UtcNow)
                {
                    storedRefreshToken.IsRevoked = true;
                    _dbContext.RefreshTokens.Update(storedRefreshToken);
                    await _dbContext.SaveChangesAsync();
                    return Failed<string>("Your session has expired. please log in again.");
                }

                if (!storedRefreshToken.IsUsed)
                {
                    storedRefreshToken.IsUsed = true;
                    _dbContext.RefreshTokens.Update(storedRefreshToken);
                }

                ApplicationUser? user = await _userManager.FindByIdAsync(userId);

                if (user is null)
                    return Failed<string>("Invalid Access Token");

                string newJwtId = Guid.NewGuid().ToString();
                string newAccessToken = await GenerateJwtToken(user, newJwtId);

                storedRefreshToken.JwtId = newJwtId;

                await _dbContext.SaveChangesAsync();

                if (newAccessToken is null)
                    return Failed<string>("FailedToGenerateNewAccessToken");

                return Success(newAccessToken, "New Access Token Created");
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.Message);
            }
        }
        private string? GetJwtIdFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
        }
        private string? GetUserIdFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value.ToString();
        }
        public async Task<ReturnBase<bool>> SendResetPasswordEmailAsync(string email)
        {
            try
            {
                if (email is null)
                    return Failed<bool>("Email is required");

                ApplicationUser? user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                    return Failed<bool>("User Not Found");

                string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string encodedToken = WebUtility.UrlEncode(resetPasswordToken);
                HttpRequest requestAccessor = _httpContextAccessor.HttpContext.Request;

                UriBuilder uriBuilder = new()
                {
                    Scheme = requestAccessor.Scheme,
                    Host = requestAccessor.Host.Host,
                    Port = requestAccessor.Host.Port ?? -1,
                    Path = "api/applicationuser/ResetPassword",
                    Query = $"email={Uri.EscapeDataString(email)}&token={encodedToken}"
                };

                string returnUrl = uriBuilder.ToString();

                string message = $"To Reset Your Password Click This Link: <a href=\"{returnUrl}\">Reset Password</a>";

                var sendEmailResult = await _emailService.SendEmailAsync(email, message, "Reset Password Link", "text/html");

                if (sendEmailResult.Succeeded)
                    return Success(true, "Reset password email send successfully");

                return Failed<bool>(sendEmailResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
        public async Task<ReturnBase<bool>> ResetPasswordAsync(string resetPasswordToken, string newPassword, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(resetPasswordToken))
                    return Failed<bool>("Invalid Token");

                ApplicationUser? user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                    return Failed<bool>("User Not Found");

                string decodedToken = WebUtility.UrlDecode(resetPasswordToken);

                IdentityResult resetPasswordResult = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);

                if (resetPasswordResult.Succeeded)
                    return Success(true, "Password has been reset successfully");

                return Failed<bool>(resetPasswordResult.Errors.FirstOrDefault().Description);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
        public async Task<ReturnBase<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(currentPassword))
                    return Failed<bool>("New and Current Passwords Are Required");

                ApplicationUser? user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return Failed<bool>("Invalid User Id");
                }

                IdentityResult changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (changePasswordResult.Succeeded)
                {
                    var sendEmailResult = await _sendPasswordChangeNotificationEmailService.SendPasswordChangeNotificationAsync(user);
                    if (!sendEmailResult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return Failed<bool>("Failed To Send Change Password Email");
                    }

                    await _signInManager.SignOutAsync();
                    await transaction.CommitAsync();
                    return Success(true, "Password has been changed successfully");
                }

                await transaction.RollbackAsync();
                return Failed<bool>("Failed To Change Password");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Failed<bool>(ex.Message);
            }
        }
    }
}
