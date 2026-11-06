using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Ratings.Commands.UpdateRating
{
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRatingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.RatingRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existing is null)
                return false; // controller will turn this into 404

            // Update only the fields that are allowed to change
            existing.Stars = request.Stars;
            existing.content = request.Content;

            await _unitOfWork.RatingRepository.UpdateAsync(existing, cancellationToken);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}