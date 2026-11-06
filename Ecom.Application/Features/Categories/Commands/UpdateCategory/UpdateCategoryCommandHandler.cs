
using MediatR;
using Ecom.Application.Interfaces;
using Ecom.Core.Services;

namespace Ecom.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManagementService _imageService;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork , IImageManagementService imageService)
        {
            _unitOfWork = unitOfWork;
        
            _imageService = imageService;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.UpdateCategoryDTO.id , cancellationToken);

            if (category is null) return false;

            // 1. Update basic info
            category.Name = request.UpdateCategoryDTO.Name;
            category.Description = request.UpdateCategoryDTO.Description;

            // 2. If a new image is provided, replace the old one
            if (request.UpdateCategoryDTO.Image != null)
            {
                // Delete old image if it exists (Optional/Best Practice)
                if (!string.IsNullOrEmpty(category.ImageUrl))
                {
                    _imageService.DeleteImageAsync(category.ImageUrl);
                }

                // Upload new image
                //var files = new FormFileCollection { request.UpdateCategoryDTO.Image };
                var path = await _imageService.AddImageAsync(request.UpdateCategoryDTO.Image, "categories");
                category.ImageUrl = path;
            }

            await _unitOfWork.CategoryRepository.UpdateAsync(category, cancellationToken);
            //await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}