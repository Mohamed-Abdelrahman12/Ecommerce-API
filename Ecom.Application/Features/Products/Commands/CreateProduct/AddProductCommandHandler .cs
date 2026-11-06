using AutoMapper;
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Products;
using MediatR;

namespace Ecom.Application.Features.Products.Commands.CreateProduct
{
    internal class AddProductCommandHandler(   
         IUnitOfWork _repo,
         IMapper _mapper,
         IImageManagementService _imageService) : IRequestHandler<AddProductCommand, bool>
         {
    
        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {

            var dto = request.ProductDto;

            var product = _mapper.Map<Product>(dto);

            await _repo.ProductRepository.AddAsync(product);

            var imagePaths = await _imageService.AddImageAsync(dto.Photo, dto.Name);

            var photos = imagePaths.Select(path => new Photo
            {
                ProductId = product.Id,
                ImageName = path
            }).ToList();

            product.Photos = photos;

            await _repo.ProductRepository.UpdateAsync(product);

            return true;
        }
    }
}
