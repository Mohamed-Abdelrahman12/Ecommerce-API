using MediatR;

namespace Ecom.Application.Features.Auth.Commands.ActiveAccount
{
    public class ActiveAccountCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
