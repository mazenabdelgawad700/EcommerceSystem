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
        IRequestHandler<SearchAboutProductQuery, ReturnBase<IQueryable<SearchAboutProductResponse>>>,
        IRequestHandler<GetProductAsPaginatedListQuery, ReturnBase<PaginatedResult<GetProductAsPaginatedListResponse>>>,
        IRequestHandler<GetRecentSearchQuery, ReturnBase<IQueryable<GetRecentSearchResponse>>>
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
        public async Task<ReturnBase<IQueryable<SearchAboutProductResponse>>> Handle(SearchAboutProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getProductsResult = _productService.SearchAboutProduct(request.SearchQuery);

                if (!getProductsResult.Succeeded)
                    return Failed<IQueryable<SearchAboutProductResponse>>(getProductsResult.Message);

                var mappedResult = _mapper.ProjectTo<SearchAboutProductResponse>(getProductsResult.Data);

                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<SearchAboutProductResponse>>(ex.InnerException.Message);
            }
        }

        public async Task<ReturnBase<IQueryable<GetRecentSearchResponse>>> Handle(GetRecentSearchQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getSearchResult = _productService.GetRecentSearchForUser(request.UserId);

                if (!getSearchResult.Succeeded)
                    return Failed<IQueryable<GetRecentSearchResponse>>(getSearchResult.Message);

                var mappedResult = _mapper.ProjectTo<GetRecentSearchResponse>(getSearchResult.Data);

                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<GetRecentSearchResponse>>(ex.InnerException.Message);
            }
        }
    }
}
