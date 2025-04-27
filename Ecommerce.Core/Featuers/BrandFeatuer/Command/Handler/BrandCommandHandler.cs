using AutoMapper;
using Ecommerce.Core.Featuers.BrandFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.BrandFeatuer.Command.Handler
{
    internal class BrandCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddBrandCommand, ReturnBase<bool>>
    {
        private readonly IImageService _imageService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandCommandHandler(IBrandService brandService, IImageService imageService, IMapper mapper)
        {
            this._brandService = brandService;
            this._imageService = imageService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var saveImage = await _imageService.SaveAsync(request.Logo);
                if (!saveImage.Succeeded)
                    return Failed<bool>(saveImage.Message);

                var mappedResult = _mapper.Map<Brand>(request);

                mappedResult.LogoUrl = saveImage.Data;

                var addBrandResult = await _brandService.AddBrandAsync(mappedResult);

                if (!addBrandResult.Succeeded)
                {
                    var removeImage = _imageService.Delete(saveImage.Data);

                    while (!removeImage.Succeeded)
                        removeImage = _imageService.Delete(saveImage.Data);

                    return Failed<bool>(addBrandResult.Message);
                }

                return Success(true);

            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
