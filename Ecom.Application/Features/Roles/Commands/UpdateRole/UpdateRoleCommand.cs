using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Application.Features.Roles.Commands.UpdateRole
{
    public record UpdateRoleCommand(string Id, string RoleName) : IRequest<IdentityResult>;
}
