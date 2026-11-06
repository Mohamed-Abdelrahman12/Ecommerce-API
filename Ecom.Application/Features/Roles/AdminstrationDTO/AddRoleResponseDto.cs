namespace Ecom.Application.Features.Roles.AdminstrationDTO
{
    public class AddRoleResponseDto
    {
        public bool IsSuccessfully { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
