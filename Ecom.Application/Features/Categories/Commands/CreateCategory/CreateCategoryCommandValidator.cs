using Ecom.Application.Features.Categories.Commands.AddCategory;
using FluentValidation;

namespace Ecom.Application.Features.Categories.Validators
{
    public class CreateCategoryCommandValidator
        : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryDTO.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.CategoryDTO.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
            RuleFor(x => x.CategoryDTO.Image)
          .NotNull().WithMessage("At least one photo is required")
          .Must(files => files.Length > 0).WithMessage("At least one photo is required");

        }
    }
}
