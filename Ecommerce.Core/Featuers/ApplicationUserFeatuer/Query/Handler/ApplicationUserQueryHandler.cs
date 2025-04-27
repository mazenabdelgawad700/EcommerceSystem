using AutoMapper;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Query.Model;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Query.Response;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Query.Handler
{
    public class ApplicationUserQueryHandler : ReturnBaseHandler,
        IRequestHandler<GetApplicationUserByIdQuery, ReturnBase<GetApplicationUserByIdResponse>>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public ApplicationUserQueryHandler(IApplicationUserService applicationUserService, IMapper mapper)
        {
            this._applicationUserService = applicationUserService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<GetApplicationUserByIdResponse>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getUserResult = await _applicationUserService.GetApplicationUserByIdAsync(request.Id);

                if (!getUserResult.Succeeded)
                    Failed<GetApplicationUserByIdResponse>(getUserResult.Message);

                var mappedResult = _mapper.Map<GetApplicationUserByIdResponse>(getUserResult.Data);

                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                return Failed<GetApplicationUserByIdResponse>(ex.InnerException.Message);
            }
        }
    }
}
