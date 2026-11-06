using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Ecom.Application.Features.Roles.Commands.AddRole
{

    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, IdentityResult>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var identityRole = new IdentityRole { Name = request.RoleName };
            return await _roleManager.CreateAsync(identityRole);
        }
    }
}
