using AutoMapper;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using MediatR;

namespace Ecom.Application.Features.Auth.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Address>(request);

            return await _unitOfWork.AuthRepository
                .UpdateAddressAsync(request.UserEmail, address, cancellationToken);
        }
    }
}
