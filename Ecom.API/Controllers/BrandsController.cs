using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.Features.Brands.Commands.CreateBrand;
using Ecom.Application.Features.Brands.Commands.DeleteBrand;
using Ecom.Application.Features.Brands.Commands.UpdateBrand;
using Ecom.Application.Features.Brands.Dto;
using Ecom.Application.Features.Brands.Queries.GetAllBrands;
using Ecom.Application.Features.Categories.Queries.GetCategoryById;
using Ecom.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class BrandsController : BaseController
    {
        private readonly IMediator _mediator;



        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper)
        {
            _mediator = mediator;
        }

        [HttpGet("Get-All")]
        public async Task<IActionResult> get(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllBrandsQuery(),cancellationToken);
            return Ok(result);
        }
     

        [HttpGet("Get-By-id/{id}")]
        public async Task<IActionResult> getById(int id, CancellationToken cancellationToken = default)
        {
            var Brand = await _mediator.Send(new GetCategoryByIdQuery(id),cancellationToken);

            if (Brand is null)
                return NotFound(new ResponseAPI(
                    StatusCodes.Status404NotFound,
                    $"Brand with id '{id}' was not found"
                ));

            return Ok(Brand);
        }

      
        [HttpPost("Add-Brand")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddBrand([FromForm] BrandDTO brandDTO, CancellationToken cancellationToken = default)
        {
             await _mediator.Send(new CreateBrandCommand(brandDTO),cancellationToken);
            return Ok(new ResponseAPI(200, "Created Successfully"));
        }
       
        [HttpPut("Update-Brand")]
        public async Task<IActionResult> UpdateBrand([FromForm] UpdateBrandDTO updateBrandDTO, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
               new UpdateBrandCommand(updateBrandDTO),cancellationToken);

            if (!result)
                return NotFound(new ResponseAPI(
                    404,
                    $"Category with id '{updateBrandDTO.Id}' was not found"
                ));

            return Ok(new ResponseAPI(
               200,
                "Item has been updated successfully"
            ));
        }

       
        [HttpDelete("Delete-Brand/{id}")]
        public async Task<IActionResult> DeleteById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id), cancellationToken);

            if (!result)
                return NotFound(new ResponseAPI(404, $"Brand with Id {id} not found"));

            return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));
        }
    }
}
