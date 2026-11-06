using FluentValidation;

namespace Ecom.Application.Features.Ratings.Commands.DeleteRating
{
    public class DeleteRatingCommandValidator : AbstractValidator<DeleteRatingCommand>
    {
        public DeleteRatingCommandValidator()
        {
            RuleFor(x => x.RatingId)
                .GreaterThan(0).WithMessage("RatingId must be a valid ID");
        }
    }
}