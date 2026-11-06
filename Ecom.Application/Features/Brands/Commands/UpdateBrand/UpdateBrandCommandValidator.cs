using FluentValidation;
namespace Ecom.Application.Features.Brands.Commands.UpdateBrand
{
    internal class UpdateBrandCommandValidator
   : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.UpdateBrandDTO.Id)
                .GreaterThan(0).WithMessage("Invalid brand id");

            RuleFor(x => x.UpdateBrandDTO.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.UpdateBrandDTO.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.UpdateBrandDTO.ImageUrl)
            .NotNull().WithMessage("At least one photo is required")
            .Must(files => files.Length > 0).WithMessage("At least one photo is required");
        }
    }
}
