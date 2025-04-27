using AutoMapper;
using Ecommerce.Core.Featuers.BrandFeatuer.Query.Model;
using Ecommerce.Core.Featuers.BrandFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.BrandFeatuer.Query.Handler
{
    public class BrandQueryHandler : ReturnBaseHandler,
        IRequestHandler<GetBrandProductsQuery, ReturnBase<PaginatedResult<GetBrandProductsResponse>>>
    {

        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandQueryHandler(IBrandService brandService, IMapper mapper)
        {
            this._brandService = brandService;
            this._mapper = mapper;
        }


        public async Task<ReturnBase<PaginatedResult<GetBrandProductsResponse>>> Handle(GetBrandProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getResult = _brandService.GetBrandProducts(request.BrandId);

                var mappedResult = await _mapper.ProjectTo<GetBrandProductsResponse>(getResult.Data).ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return getResult.Succeeded ? Success(mappedResult) : Failed<PaginatedResult<GetBrandProductsResponse>>(getResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<PaginatedResult<GetBrandProductsResponse>>(ex.InnerException.Message);
            }
        }
    }
}
