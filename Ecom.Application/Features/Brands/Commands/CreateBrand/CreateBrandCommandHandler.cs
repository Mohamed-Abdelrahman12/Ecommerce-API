
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Products;
using MediatR;

namespace Ecom.Application.Features.Brands.Commands.CreateBrand
{
    internal class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IImageManagementService _imageService;
        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IImageManagementService imageService)
        {
            _unitOfWork = unitOfWork;

            _imageService = imageService;
        }
        public async Task<bool> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {

            var imagePaths = await _imageService.AddImageAsync(request.BrandDTO.ImageUrl, "Brands");

            var brandEntity = new Brand
            {
                Name = request.BrandDTO.Name,
                Description = request.BrandDTO.Description,
                ImageUrl = imagePaths
            };

            await _unitOfWork.BrandRepository.AddAsync(brandEntity, cancellationToken);

            return true;
        }
    }
}
