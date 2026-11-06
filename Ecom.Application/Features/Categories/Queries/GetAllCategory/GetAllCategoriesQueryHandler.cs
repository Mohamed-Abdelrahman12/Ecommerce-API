using AutoMapper;
using Ecom.Application.Features.Categories.Dto;
using Ecom.Application.Features.Categories.Queries.GetAllCategory;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using MediatR;

namespace Ecom.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler
        : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryResponseDTO>> Handle(
            GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var Category = await _unitOfWork.CategoryRepository.GetAllAsync();
            var maping= _mapper.Map<IReadOnlyList<CategoryResponseDTO>>(Category);
            return maping;
        }
    }
}
