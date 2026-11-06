using Ecom.Application.Features.Categories.Commands.UpdateCategory;
using FluentValidation;

namespace Ecom.Application.Features.Categories.Validators
{
    public class UpdateCategoryCommandValidator
        : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.UpdateCategoryDTO.id)
                .GreaterThan(0).WithMessage("Invalid category id");

            RuleFor(x => x.UpdateCategoryDTO.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.UpdateCategoryDTO.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.UpdateCategoryDTO.Image)
     .NotNull().WithMessage("At least one photo is required")
     .Must(files => files.Length > 0).WithMessage("At least one photo is required");
        }
    }
}
