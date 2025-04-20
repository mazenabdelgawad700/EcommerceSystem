using AutoMapper;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Handler
{
    public class ApplicationUserCommandHandler : ReturnBaseHandler, IRequestHandler<RegisterApplicationUserCommand, ReturnBase<bool>>, IRequestHandler<ConfirmEmailCommand, ReturnBase<bool>>
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

    }
}
