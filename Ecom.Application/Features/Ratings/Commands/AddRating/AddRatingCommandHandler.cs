using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Ratings.Commands.AddRating
{
    public class AddRatingCommandHandler : IRequestHandler<AddRatingCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRatingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            // All business logic delegated to the repository
            // UnitOfWork exposes the RatingRepository
            var result = await _unitOfWork.RatingRepository.AddRatingAsync(
                stars: request.Stars,
                content: request.Content,
                productId: request.ProductId,
                userEmail: request.UserEmail,
                cancellationToken: cancellationToken
            );

            // SaveChanges is managed inside the repository for this use case
            // because it needs two separate saves (rating then product average).
            // If you move to a single-save pattern, call _unitOfWork.SaveChangesAsync() here.
            return result;
        }
    }
}