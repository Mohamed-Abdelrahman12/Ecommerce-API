using FluentValidation;

namespace Ecom.Application.Features.Ratings.Commands.AddRating
{
    public class AddRatingCommandValidator : AbstractValidator<AddRatingCommand>
    {
        public AddRatingCommandValidator()
        {
            RuleFor(x => x.Stars)
                .NotEmpty().WithMessage("Stars is required")
                .InclusiveBetween(1, 5).WithMessage("Stars must be between 1 and 5");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(500).WithMessage("Content must not exceed 500 characters");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be a valid ID");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("User email is required")
                .EmailAddress().WithMessage("User email is not valid");
        }
    }
}