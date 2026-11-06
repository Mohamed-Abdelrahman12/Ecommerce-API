using Ecom.Application.Features.Categories.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(
       UpdateCategoryDTO UpdateCategoryDTO
       ) : IRequest<bool>;
}
