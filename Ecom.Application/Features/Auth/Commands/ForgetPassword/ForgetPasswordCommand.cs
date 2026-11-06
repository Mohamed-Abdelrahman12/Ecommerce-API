using MediatR;

namespace Ecom.Application.Features.Auth.Commands.ForgetPassword
{
    public class ForgetPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
