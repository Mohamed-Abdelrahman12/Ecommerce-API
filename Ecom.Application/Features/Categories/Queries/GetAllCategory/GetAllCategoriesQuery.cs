using Ecom.Application.Features.Categories.Dto;
using Ecom.Domain.Entities.Products;
using MediatR;

namespace Ecom.Application.Features.Categories.Queries.GetAllCategory
{
    public record GetAllCategoriesQuery : IRequest<IReadOnlyList<CategoryResponseDTO>>;
}
