using FluentValidation;


namespace Ecom.Application.Features.Contacts.Commands.DeleteMessage
{
    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required.")
                .GreaterThan(0).WithMessage("ID must be a positive number.");
        }
    }
}
