using FluentValidation;
using Ecom.Application.Features.Categories.Commands.DeleteCategory;

namespace Ecom.Application.Features.Categories.Validators
{
    public class DeleteCategoryCommandValidator
        : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0)
                .WithMessage("Invalid category id");
        }
    }
}
