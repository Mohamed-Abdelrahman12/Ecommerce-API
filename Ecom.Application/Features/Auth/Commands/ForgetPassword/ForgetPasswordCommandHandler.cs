using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Auth.Commands.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ForgetPasswordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthRepository
                .SendEmailForForgetPasswordAsync(request.Email, cancellationToken);
        }
    }
}
