using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.Features.Products.Commands.CreateProduct;
using Ecom.Application.Features.Products.Commands.DeleteProduct;
using Ecom.Application.Features.Products.Commands.UpdateProduct;
using Ecom.Application.Features.Products.Dto;
using Ecom.Application.Features.Products.Queries.GetAllProducts;
using Ecom.Application.Features.Products.Queries.GetProductDetails;
using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper)
        {
            _mediator = mediator;
        }
        [HttpGet("Get-All-Sorting")]
        public async Task<IActionResult> GetAll([FromQuery] ProductParams productParams)
        {
            
            var result = await _mediator.Send(new GetAllProductsQuery(productParams));
            return Ok(result);
        }
      
        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetAllAsync(cancellationToken,x=>  x.Category, x=> x.Brand,x=>x.Photos);

                var Result = _mapper.Map<List< ProductDTO>>(product);

                if (product is null) {
                    BadRequest(new ResponseAPI(400));
                }

                return Ok(Result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get-By-Id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _mediator.Send(new GetProductByIdQuery(id, 
                x => x.Category, x => x.Photos,x=> x.Brand));
            return Ok(result);
        }
       
        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDTO addProductDTO)
        {
            var result = await _mediator.Send(new AddProductCommand(addProductDTO));
            return result
                ? Ok(new ResponseAPI(200, "Added Product Successfully"))
                : BadRequest(new ResponseAPI(400));
        }

    

        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            var result = await _mediator.Send(new UpdateProductCommand(updateProductDTO));
            return result
                ? Ok(new ResponseAPI(200, "Updated Product Successfully"))
                : BadRequest(new ResponseAPI(400));
        }

      
        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteProductCommand(id));

                if (!result)
                    return NotFound(new ResponseAPI(404, "Product Not Found"));

                return Ok(new ResponseAPI(200, "Deleted Product Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }


    }
}
