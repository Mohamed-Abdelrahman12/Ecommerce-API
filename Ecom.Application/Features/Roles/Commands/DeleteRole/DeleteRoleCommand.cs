using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Application.Features.Roles.Commands.DeleteRole
{
    public record DeleteRoleCommand(string RoleId) : IRequest<IdentityResult>;
}
