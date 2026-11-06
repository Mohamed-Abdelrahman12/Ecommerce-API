using Ecom.Application.Features.Auth.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var dto = new ResetPasswordDTO
            {
                Email = request.Email,
                Token = request.Token,
                Password = request.Password,
            };

            return await _unitOfWork.AuthRepository.ResetPasswordAsync(dto, cancellationToken);
        }
    }
}
