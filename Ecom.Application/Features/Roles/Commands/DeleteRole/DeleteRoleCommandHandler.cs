using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IdentityResult>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role Not Exist" });
            }

            return await _roleManager.DeleteAsync(role);
        }
    }
}
