using AutoMapper;
using Ecom.Application.Common.Filters;
using Ecom.Application.Features.Products.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ReturnProductDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReturnProductDTO> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.ProductRepository.GetAllAsync();
            var query = products.AsQueryable();

            var rules = new List<IProductFilter>
            {
                new SearchFilter(),
                new CategoryFilter(),
                new BrandFilter(),
                new RatingFilter(),
                new SortingFilter()
            };

            foreach (var rule in rules)
                query = rule.Apply(query, request.Params);

            var totalCount = query.Count();

            query = query
                .Skip((request.Params.PageNumber - 1) * request.Params.pageSize)
                .Take(request.Params.pageSize);

            var dto = new ReturnProductDTO
            {
                TotalCount = totalCount,
                products = _mapper.Map<List<ProductDTO>>(query)
            };

            return dto;
        }
    }
}
