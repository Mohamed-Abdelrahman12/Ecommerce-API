using FluentValidation;

namespace Ecom.Application.Features.Auth.Commands.ActiveAccount
{
    public class ActiveAccountCommandValidator : AbstractValidator<ActiveAccountCommand>
    {
        public ActiveAccountCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Activation token is required");
        }
    }
}
