

using Ecom.Application.Features.Auth.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Auth.Commands.ActiveAccount
{

    public class ActiveAccountCommandHandler : IRequestHandler<ActiveAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActiveAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ActiveAccountCommand request, CancellationToken cancellationToken)
        {
            var dto = new ActiveAccountDTO
            {
                Email = request.Email,
                Token = request.Token,
            };

            return await _unitOfWork.AuthRepository.ActiveAccountAsync(dto, cancellationToken);
        }
    }
}
