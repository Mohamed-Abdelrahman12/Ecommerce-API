using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Ecom.Application.Features.Roles.Commands.AddRole
{
    public record AddRoleCommand(string RoleName) : IRequest<IdentityResult>;
}
