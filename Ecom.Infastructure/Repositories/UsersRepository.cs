using Ecom.Application.Features.Roles.AdminstrationDTO;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<AppUserDto>> GetAllUsers()
        {
            var UsersList = await _userManager.Users.AsNoTracking().ToListAsync();
            var user = UsersList.Select(u => new AppUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserRoles =  _userManager.GetRolesAsync(u).Result.ToArray()

            });
            return user;
        }
        public async Task<AppUser> GetUserByName(string name)
        {
            var User = await _userManager.FindByNameAsync(name);
            return User;
        }
        public async Task<AppUser> GetUserByEmail(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User;
        }

        public async Task<UsersRolesDto> GetUserWithRoles(string UserId)
        {
            var User = await _userManager.FindByIdAsync(UserId);

            if (User is null)
            {
                return null;
            }
            var _roles = await _roleManager.Roles.AsNoTracking().ToListAsync();

            var userRoles = new UsersRolesDto()
            {
                Id = User.Id,
                UserName = User.UserName,
                Roles = _roles.Select(r => new SelectedRulesDto
                {
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(User, r.Name).Result
                }).ToList()
            };
            return userRoles;
        }

        public async Task<bool> AddUserRoles(UsersRolesDto usersRoles)
        {
            var _user = await _userManager.FindByIdAsync(usersRoles.Id);
            if (_user is null)
            {
                return false;
            }

            foreach (var role in usersRoles.Roles)
            {
                if (role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(_user, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(_user, role.Name);
                }
            }
            return true;
        }
    }
}
