using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Auth.Queries.GetUserAddress
{
    public class GetUserAddressQueryHandler : IRequestHandler<GetUserAddressQuery, ShippingAddressDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShippingAddressDTO> Handle(GetUserAddressQuery request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.AuthRepository
                .GetUserAddressAsync(request.UserEmail, cancellationToken);

            // Returns null-safe mapped DTO — controller returns 404 if null
            return _mapper.Map<ShippingAddressDTO>(address);
        }
    }
}
