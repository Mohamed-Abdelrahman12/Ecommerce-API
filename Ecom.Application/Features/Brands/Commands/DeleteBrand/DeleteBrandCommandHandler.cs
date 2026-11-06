using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Brands.Commands.DeleteBrand
{
    internal class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository
               .GetByIdAsync(request.id,cancellationToken);

            if (brand is null)
                return false;

            await _unitOfWork.BrandRepository.DeleteAsync(request.id);
            return true;
        }
    }
}
