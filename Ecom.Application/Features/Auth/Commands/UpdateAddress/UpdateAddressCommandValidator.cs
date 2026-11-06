using FluentValidation;
namespace Ecom.Application.Features.Auth.Commands.UpdateAddress
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("User email is required")
                .EmailAddress().WithMessage("User email is not valid");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required")
                .MaximumLength(150).WithMessage("Street must not exceed 150 characters");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(100).WithMessage("City must not exceed 100 characters");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(100).WithMessage("Country must not exceed 100 characters");
        }
    }
}
