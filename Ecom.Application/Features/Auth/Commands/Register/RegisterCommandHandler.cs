using Ecom.Application.DTOs;
using Ecom.Application.Features.Auth.Dto;
using Ecom.Application.Interfaces;
using MediatR;
namespace Ecom.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var dto = new RegisterDTO
            {
                UserName = request.UserName,
                DisplayName = request.DisplayName,
                Email = request.Email,
                Password = request.Password,
            };

            return await _unitOfWork.AuthRepository.RegisterAsync(dto, cancellationToken);
        }
    }
}
