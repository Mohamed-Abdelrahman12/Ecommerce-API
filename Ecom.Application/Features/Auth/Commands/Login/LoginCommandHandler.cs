using Ecom.Application.DTOs;
using Ecom.Application.Features.Auth.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = new LoginDTO
            {
                Email = request.Email,
                Password = request.Password,
            };

            return await _unitOfWork.AuthRepository.LoginAsync(dto, cancellationToken);
        }
    }
}
