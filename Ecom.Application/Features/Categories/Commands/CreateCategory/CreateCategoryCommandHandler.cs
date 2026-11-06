
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Products;
using MediatR;

namespace Ecom.Application.Features.Categories.Commands.AddCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
      
        private readonly IImageManagementService _imageService;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IImageManagementService imageService)
        {
            _unitOfWork = unitOfWork;

            _imageService = imageService;
        }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
           
            var imagePaths = await _imageService.AddImageAsync(request.CategoryDTO.Image, "categories");

            var categoryEntity = new Category
            {
                Name = request.CategoryDTO.Name,
                Description = request.CategoryDTO.Description,
                ImageUrl = imagePaths
            };

            await _unitOfWork.CategoryRepository.AddAsync(categoryEntity, cancellationToken);

            return true;
        }
    }

}
