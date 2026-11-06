using AutoMapper;
using Ecom.Application.Features.Brands.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Brands.Queries.GetAllBrands
{
    internal class GetAllBrandsQueryHandler
        : IRequestHandler<GetAllBrandsQuery, IReadOnlyList<BrandResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllBrandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BrandResponseDTO>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var Brand = await _unitOfWork.BrandRepository.GetAllAsync(cancellationToken);
            var maping = _mapper.Map<IReadOnlyList<BrandResponseDTO>>(Brand);
            return maping;
        }
    }
}
