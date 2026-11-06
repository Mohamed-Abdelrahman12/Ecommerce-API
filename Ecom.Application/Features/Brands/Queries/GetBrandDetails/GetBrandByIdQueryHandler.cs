using AutoMapper;
using Ecom.Application.Features.Brands.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Brands.Queries.GetBrandDetails
{
    internal class GetBrandByIdQueryHandler
        : IRequestHandler<GetBrandByIdQuery, BrandDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBrandByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BrandDTO> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var GetBrandId = await _unitOfWork.BrandRepository
           .GetByIdAsync(request.Id, cancellationToken);

            var map = _mapper.Map<BrandDTO>(GetBrandId);

            return map;
        }
    }
}
