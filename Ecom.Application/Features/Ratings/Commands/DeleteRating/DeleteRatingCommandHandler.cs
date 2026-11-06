using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Ratings.Commands.DeleteRating
{
    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRatingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            // Check the rating exists before deleting
            var existing = await _unitOfWork.RatingRepository.GetByIdAsync(request.RatingId, cancellationToken);

            if (existing is null)
                return false; // controller will turn this into 404

            await _unitOfWork.RatingRepository.DeleteAsync(request.RatingId, cancellationToken);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}