using FluentValidation;

namespace Ecom.Application.Features.WishList.Commands.AddToWishList
{
    public class AddToWishListCommandValidator : AbstractValidator<AddToWishListCommand>
    {
        public AddToWishListCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be a valid ID");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("User email is required")
                .EmailAddress().WithMessage("User email is not valid");
        }
    }
}