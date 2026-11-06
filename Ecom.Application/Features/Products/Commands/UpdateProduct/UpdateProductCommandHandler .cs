using AutoMapper;
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Products;
using MediatR;
namespace Ecom.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageService;

        public UpdateProductCommandHandler(IUnitOfWork repo, IMapper mapper, IImageManagementService imageService)
        {
            _repo = repo;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDto;

            var product = await _repo.ProductRepository.GetByIdAsync(
                dto.Id,
                cancellationToken,
                p => p.Photos,
                p => p.Category
            );

            if (product == null)
                return false;

            // Map new data to entity
            _mapper.Map(dto, product);

            // Delete old images from DB + FileSystem
            await _repo.ProductRepository.RemoveProductImagesAsync(product.Id);

            // Add new images
            var newImagePaths = await _imageService.AddImageAsync(dto.Photo, dto.Name);

            product.Photos = newImagePaths
                .Select(path => new Photo { ImageName = path, ProductId = product.Id })
                .ToList();

            await _repo.ProductRepository.UpdateAsync(product);

            return true;
        }

    }
}