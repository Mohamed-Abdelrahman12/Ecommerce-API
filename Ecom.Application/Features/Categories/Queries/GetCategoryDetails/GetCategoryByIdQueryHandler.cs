using AutoMapper;
using Ecom.Application.Features.Categories.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler
        : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDTO> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var GetCategroyId= await _unitOfWork.CategoryRepository
                .GetByIdAsync(request.Id);

            var map = _mapper.Map<CategoryResponseDTO>(GetCategroyId);

            return map;
        }
    }
}
