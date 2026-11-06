using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Application.Features.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery : IRequest<IEnumerable<IdentityRole>>;
}
