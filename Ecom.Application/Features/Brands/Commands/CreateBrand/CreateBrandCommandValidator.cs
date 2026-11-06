using FluentValidation;
namespace Ecom.Application.Features.Brands.Commands.CreateBrand
{
    internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.BrandDTO.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.BrandDTO.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
            RuleFor(x => x.BrandDTO.ImageUrl)
             .NotNull().WithMessage("At least one photo is required")
             .Must(files => files.Length > 0).WithMessage("At least one photo is required");
        }
    }
}

