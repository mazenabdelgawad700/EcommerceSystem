﻿using AutoMapper;
using Ecommerce.Core.Featuers.BrandFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.BrandFeatuer.Command.Handler
{
    internal class BrandCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddBrandCommand, ReturnBase<bool>>,
        IRequestHandler<UpdateBrandCommand, ReturnBase<bool>>,
        IRequestHandler<DeleteBrandCommand, ReturnBase<bool>>
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
        public async Task<ReturnBase<bool>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Logo is not null)
                {
                    var removeImage = _imageService.Delete(request.OldLogoUrl);

                    while (!removeImage.Succeeded)
                        removeImage = _imageService.Delete(request.OldLogoUrl);

                    var saveImage = await _imageService.SaveAsync(request.Logo);

                    if (!saveImage.Succeeded)
                        return Failed<bool>(saveImage.Message);

                    var mappedResult = _mapper.Map<Brand>(request);

                    mappedResult.LogoUrl = saveImage.Data;

                    var addBrandResult = await _brandService.UpdateBrandAsync(mappedResult);

                    if (!addBrandResult.Succeeded)
                    {
                        var removeImageFromServer = _imageService.Delete(saveImage.Data);

                        while (!removeImageFromServer.Succeeded)
                            removeImageFromServer = _imageService.Delete(saveImage.Data);

                        return Failed<bool>(addBrandResult.Message);
                    }

                }
                else
                {
                    var mappedResult = _mapper.Map<Brand>(request);

                    mappedResult.LogoUrl = request.OldLogoUrl;

                    var addBrandResult = await _brandService.UpdateBrandAsync(mappedResult);

                    if (!addBrandResult.Succeeded)
                        return Failed<bool>(addBrandResult.Message);
                }


                return Success(true);

            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }

        }
        public async Task<ReturnBase<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteResult = await _brandService.DeleteBrandAsync(request.Id);
                return deleteResult.Succeeded ? Success(true) : Failed<bool>(deleteResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
