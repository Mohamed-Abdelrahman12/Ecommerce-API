using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, IdentityResult>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role Not Exist" });
            }

            role.Name = request.RoleName;
            return await _roleManager.UpdateAsync(role);
        }
    }
}
