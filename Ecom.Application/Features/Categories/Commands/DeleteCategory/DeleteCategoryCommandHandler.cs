using MediatR;
using Ecom.Application.Interfaces;

namespace Ecom.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand , bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetByIdAsync(request.id,cancellationToken);

            if (category is null)
                return false;

            await _unitOfWork.CategoryRepository.DeleteAsync(request.id, cancellationToken);
            return true;
        }

    }
}
