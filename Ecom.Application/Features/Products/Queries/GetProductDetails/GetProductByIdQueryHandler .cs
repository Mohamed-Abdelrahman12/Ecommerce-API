using AutoMapper;
using Ecom.Application.Features.Products.Dto;
using Ecom.Application.Interfaces;
using MediatR;


namespace Ecom.Application.Features.Products.Queries.GetProductDetails
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.ProductRepository.GetByIdAsync(
                 request.Id,
                 cancellationToken,
               request.Includes
             );

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

            return _mapper.Map<ProductDTO>(product);
        }
    }
}
