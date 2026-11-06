using Ecom.Application.DTOs;
using Ecom.Application.Features.Products.Commands.Validators;
using FluentValidation;

namespace Ecom.Application.Features.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.ProductDto.Id)
                .GreaterThan(0).WithMessage("Product ID must be greater than zero.");
        }

        
    }
}
