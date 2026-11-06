using FluentValidation;

namespace Ecom.Application.Features.WishList.Commands.DeleteWishList
{
    public class DeleteWishListCommandValidator : AbstractValidator<DeleteWishListCommand>
    {
        public DeleteWishListCommandValidator()
        {
            RuleFor(x => x.WishListId)
                .GreaterThan(0).WithMessage("WishListId must be a valid ID");
        }
    }
}