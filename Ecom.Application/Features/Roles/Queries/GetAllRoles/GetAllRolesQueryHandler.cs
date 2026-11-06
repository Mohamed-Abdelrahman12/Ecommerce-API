using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Ecom.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityRole>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return _roleManager.Roles.ToList();
        }
    }
}
