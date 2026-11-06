using Ecom.Application.Features.Categories.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Features.Categories.Commands.AddCategory
{
    public record CreateCategoryCommand(CategoryDTO CategoryDTO): IRequest<bool>;


}
