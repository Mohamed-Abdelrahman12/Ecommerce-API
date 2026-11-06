

using AutoMapper;
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using MediatR;

namespace Ecom.Application.Features.Brands.Commands.UpdateBrand
{
    internal class UpdateBrandCommandHandler
       : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManagementService _imageService;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IImageManagementService imageService)
        {
            _unitOfWork = unitOfWork;

            _imageService = imageService;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(request.UpdateBrandDTO.Id, cancellationToken);

            if (brand is null) return false;

            brand.Name = request.UpdateBrandDTO.Name;
            brand.Description = request.UpdateBrandDTO.Description;

            if (request.UpdateBrandDTO.ImageUrl != null)
            {
                if (!string.IsNullOrEmpty(brand.ImageUrl))
                {
                    _imageService.DeleteImageAsync(brand.ImageUrl);
                }

                var path = await _imageService.AddImageAsync(request.UpdateBrandDTO.ImageUrl, "Brands");
                brand.ImageUrl = path;
            }
            await _unitOfWork.BrandRepository.UpdateAsync(brand, cancellationToken);

            return true;

        }
    }
}
