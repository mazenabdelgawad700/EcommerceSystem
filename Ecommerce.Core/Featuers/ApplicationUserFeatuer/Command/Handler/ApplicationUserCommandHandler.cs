using AutoMapper;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Handler
{
    public class ApplicationUserCommandHandler : ReturnBaseHandler,
        IRequestHandler<RegisterApplicationUserCommand, ReturnBase<bool>>,
        IRequestHandler<ConfirmEmailCommand, ReturnBase<bool>>,
        IRequestHandler<LoginApplicationUserCommand, ReturnBase<string>>,
        IRequestHandler<RefreshTokenCommand, ReturnBase<string>>,
        IRequestHandler<ResetPasswordCommand, ReturnBase<bool>>,
        IRequestHandler<SendResetPasswordEmailCommand, ReturnBase<bool>>,
        IRequestHandler<ChangePasswordCommand, ReturnBase<bool>>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IConfirmEmailService _confirmEmailService;
        private readonly IMapper _mapper;

        public ApplicationUserCommandHandler(IApplicationUserService applicationUserService, IMapper mapper, IConfirmEmailService confirmEmailService)
        {
            this._applicationUserService = applicationUserService;
            this._mapper = mapper;
            _confirmEmailService = confirmEmailService;
        }

        public async Task<ReturnBase<bool>> Handle(RegisterApplicationUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<ApplicationUser>(request);

                var addUserResult = await _applicationUserService.RegisterApplicationUserAsync(mappedResult, request.Password, request.Role);

                if (!addUserResult.Succeeded)
                    return Failed<bool>(addUserResult.Message);

                return Success(true, addUserResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
        public async Task<ReturnBase<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ReturnBase<bool> confrimEmailResult = await _confirmEmailService.ConfirmEmailAsync(request.UserId, request.Token);

                if (confrimEmailResult.Succeeded)
                {
                    return Success(true, confrimEmailResult.Message);
                }
                return Failed<bool>(confrimEmailResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
        public async Task<ReturnBase<string>> Handle(LoginApplicationUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loginResult = await _applicationUserService.LoginAsync(request.Email, request.Password);

                if (!loginResult.Succeeded)
                    return Failed<string>(loginResult.Message);

                return Success(loginResult.Data, loginResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.Message);
            }
        }
        public async Task<ReturnBase<string>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var refreshTokenResult = await _applicationUserService.RefreshTokenAsync(request.AccessToken);

                if (!refreshTokenResult.Succeeded)
                    return Failed<string>(refreshTokenResult.Message);

                return Success(refreshTokenResult.Data, refreshTokenResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.Message);
            }
        }

        public async Task<ReturnBase<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resetPasswordResult = await _applicationUserService.ResetPasswordAsync(request.ResetPasswordToken, request.NewPassword, request.Email);

                if (!resetPasswordResult.Succeeded)
                    return Failed<bool>(resetPasswordResult.Message);

                return Success(resetPasswordResult.Data, resetPasswordResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
        public async Task<ReturnBase<bool>> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sendResentPasswordEmailResult = await _applicationUserService.SendResetPasswordEmailAsync(request.Email);

                if (!sendResentPasswordEmailResult.Succeeded)
                    return Failed<bool>(sendResentPasswordEmailResult.Message);

                return Success(sendResentPasswordEmailResult.Data, sendResentPasswordEmailResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
        public async Task<ReturnBase<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var changePasswordResult = await _applicationUserService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);

                if (!changePasswordResult.Succeeded)
                    return Failed<bool>(changePasswordResult.Message);

                return Success(changePasswordResult.Data, changePasswordResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }
        }
    }
}
