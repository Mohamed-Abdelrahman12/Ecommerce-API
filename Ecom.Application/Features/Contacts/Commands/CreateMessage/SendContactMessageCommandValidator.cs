using FluentValidation;

namespace Ecom.Application.Features.Contacts.Commands.CreateMessage
{
    internal class SendContactMessageCommandValidator : AbstractValidator<SendContactMessageCommand>
    {
        public SendContactMessageCommandValidator()
        {
            // Validating the DTO inside the Command
            RuleFor(x => x.ContactDto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.ContactDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.ContactDto.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters.");

            RuleFor(x => x.ContactDto.Message)
                .NotEmpty().WithMessage("Message body is required.")
                .MinimumLength(10).WithMessage("Message must be at least 10 characters long.")
                .MaximumLength(2000).WithMessage("Message cannot exceed 2000 characters.");
        }
    }
}
