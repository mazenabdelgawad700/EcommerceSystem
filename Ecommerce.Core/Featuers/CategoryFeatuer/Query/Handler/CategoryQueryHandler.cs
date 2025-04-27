using AutoMapper;
using Ecommerce.Core.Featuers.CategoryFeatuer.Query.Model;
using Ecommerce.Core.Featuers.CategoryFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CategoryFeatuer.Query.Handler
{
    public class CategoryQueryHandler : ReturnBaseHandler,
        IRequestHandler<GetCategoryProductsQuery, ReturnBase<PaginatedResult<GetCategoryProductsResponse>>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryQueryHandler(ICategoryService categoryService, IMapper mapper)
        {
            this._categoryService = categoryService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<PaginatedResult<GetCategoryProductsResponse>>> Handle(GetCategoryProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getProductsResult = _categoryService.GetCategoryProductsAsync(request.CategoryId);

                if (!getProductsResult.Succeeded)
                    return Failed<PaginatedResult<GetCategoryProductsResponse>>(getProductsResult.Message);

                var mappedResult = await _mapper.ProjectTo<GetCategoryProductsResponse>(getProductsResult.Data)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                return Failed<PaginatedResult<GetCategoryProductsResponse>>(ex.InnerException.Message);
            }
        }
    }
}
