using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ecom.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public Address Adress { get; set; }
        public string DispalyName { get; set; }
        [NotMapped]
        public string? UserRoles { get; set; }
    }
    public class AppUserDto {

     public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string?[] UserRoles { get; set; }

    }
}
