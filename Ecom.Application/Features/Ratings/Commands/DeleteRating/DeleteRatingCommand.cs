using MediatR;

namespace Ecom.Application.Features.Ratings.Commands.DeleteRating
{
    // IRequest<bool> — handler returns true if deleted, false if not found
    public class DeleteRatingCommand : IRequest<bool>
    {
        public int RatingId { get; set; }

        public DeleteRatingCommand(int ratingId)
        {
            RatingId = ratingId;
        }
    }
}