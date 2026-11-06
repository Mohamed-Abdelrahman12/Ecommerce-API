using FluentValidation;

namespace Ecom.Application.Features.Ratings.Commands.UpdateRating
{
    public class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
    {
        public UpdateRatingCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Rating ID must be a valid ID");

            RuleFor(x => x.Stars)
                .NotEmpty().WithMessage("Stars is required")
                .InclusiveBetween(1, 5).WithMessage("Stars must be between 1 and 5");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(500).WithMessage("Content must not exceed 500 characters");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be a valid ID");
        }
    }
}