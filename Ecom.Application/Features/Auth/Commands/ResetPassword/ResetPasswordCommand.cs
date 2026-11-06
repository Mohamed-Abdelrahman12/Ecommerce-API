using MediatR;

namespace Ecom.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
