using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Features.Brands.Commands.DeleteBrand
{
    internal class DeleteBrandCommandValidator
     : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0)
                .WithMessage("Invalid category id");
        }
    }
}
