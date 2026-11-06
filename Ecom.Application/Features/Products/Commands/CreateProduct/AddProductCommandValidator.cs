using Ecom.Application.Features.Products.Commands.CreateProduct;
using FluentValidation;

namespace Ecom.Application.Features.Products.Commands.Validators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.ProductDto.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

            RuleFor(x => x.ProductDto.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.ProductDto.NewPrice)
                .GreaterThan(0).WithMessage("New price must be greater than 0");

            RuleFor(x => x.ProductDto.OldPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Old price must be greater or equal to 0");

            RuleFor(x => x.ProductDto.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be valid");

            RuleFor(x => x.ProductDto.BrandId)
                .GreaterThan(0).WithMessage("BrandId must be valid");

            RuleFor(x => x.ProductDto.Photo)
                .NotNull().WithMessage("At least one photo is required")
                .Must(files => files.Count > 0).WithMessage("At least one photo is required");
        }
    }
}
