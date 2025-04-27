using AutoMapper;
using Ecommerce.Core.Featuers.CategoryFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CategoryFeatuer.Command.Handler
{
    internal class CategoryCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddCategoryCommand, ReturnBase<bool>>,
        IRequestHandler<DeleteCategoryCommand, ReturnBase<bool>>,
        IRequestHandler<UpdateCategoryCommand, ReturnBase<bool>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
        {
            this._categoryService = categoryService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<Category>(request);
                var addResult = await _categoryService.AddCategoryAsync(mappedResult);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }

        public async Task<ReturnBase<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteResult = await _categoryService.DeleteCategoryAsync(request.Id);
                return deleteResult.Succeeded ? Success(true) : Failed<bool>(deleteResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }

        public async Task<ReturnBase<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<Category>(request);
                var updateResult = await _categoryService.UpdateCategoryAsync(mappedResult);
                return updateResult.Succeeded ? Success(true) : Failed<bool>(updateResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
