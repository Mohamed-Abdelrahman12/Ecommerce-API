using Ecom.Application.Features.Roles.AdminstrationDTO;
using Ecom.Domain.Entities;

namespace Ecom.Application.Interfaces
{
    public interface IUsersRepository
    {
        
        Task<IEnumerable<AppUserDto>> GetAllUsers();
        Task<AppUser> GetUserByName(string name);
        Task<AppUser> GetUserByEmail(string email);
        Task<UsersRolesDto> GetUserWithRoles(string UserId);
        Task<bool> AddUserRoles(UsersRolesDto usersRoles);
    }
}
