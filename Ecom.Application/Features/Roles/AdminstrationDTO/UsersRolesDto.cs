namespace Ecom.Application.Features.Roles.AdminstrationDTO
{
    public class UsersRolesDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public List<SelectedRulesDto> Roles { get; set; }
    }
}
