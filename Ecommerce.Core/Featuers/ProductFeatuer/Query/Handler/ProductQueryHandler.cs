using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Model;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Query.Handler
{
    public class ProductQueryHandler : ReturnBaseHandler,
        IRequestHandler<GetProductByIdQuery, ReturnBase<GetProductByIdResponse>>,
        IRequestHandler<GetProductAsPaginatedListQuery, ReturnBase<PaginatedResult<GetProductAsPaginatedListResponse>>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public ProductQueryHandler(IProductService productService, IMapper mapper)
        {
            this._productService = productService;
            this._mapper = mapper;
        }


        public async Task<ReturnBase<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getProductResult = await _productService.GetProductByIdAsync(request.Id);

                if (!getProductResult.Succeeded)
                    return Failed<GetProductByIdResponse>(getProductResult.Message);

                var mappedResult = _mapper.Map<GetProductByIdResponse>(getProductResult.Data);

                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                return Failed<GetProductByIdResponse>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<PaginatedResult<GetProductAsPaginatedListResponse>>> Handle(GetProductAsPaginatedListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getProductsResult = _productService.GetProductsAsPaginated(request.CategoryId ?? null, request.BrandId ?? null);

                if (!getProductsResult.Succeeded)
                    return Failed<PaginatedResult<GetProductAsPaginatedListResponse>>(getProductsResult.Message);

                var mappedResult = await _mapper.ProjectTo<GetProductAsPaginatedListResponse>(getProductsResult.Data).ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                return Failed<PaginatedResult<GetProductAsPaginatedListResponse>>(ex.InnerException.Message);
            }
        }
    }
}
