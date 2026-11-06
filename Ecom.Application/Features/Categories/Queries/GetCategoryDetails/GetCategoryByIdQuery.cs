using MediatR;
using Ecom.Application.Features.Categories.Dto;

namespace Ecom.Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryResponseDTO>;
}
