using FluentValidation;
namespace Ecom.Application.Features.Products.Commands.DeleteProduct
{
    internal class DeleteProductCommandValidator
        : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Invalid category id");
        }
    }
}
