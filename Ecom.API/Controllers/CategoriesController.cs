using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.Features.Categories.Commands.AddCategory;
using Ecom.Application.Features.Categories.Commands.DeleteCategory;
using Ecom.Application.Features.Categories.Commands.UpdateCategory;
using Ecom.Application.Features.Categories.Dto;
using Ecom.Application.Features.Categories.Queries.GetAllCategory;
using Ecom.Application.Features.Categories.Queries.GetCategoryById;
using Ecom.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;



        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper)
        {
            _mediator = mediator;
        }
        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery(),cancellationToken);
            return Ok(result);
        }
        [HttpGet("Get-By-Id/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id),cancellationToken);

            if (category is null)
                return NotFound(new ResponseAPI(
                    StatusCodes.Status404NotFound,
                    $"Category with id '{id}' was not found"
                ));

            return Ok(category);
        }

      
        [HttpPost("Add-Category")]
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> Create([FromForm] CategoryDTO categoryDTO, CancellationToken cancellationToken)
        {
            var command = new CreateCategoryCommand(categoryDTO);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new ResponseAPI(200, "Created Successfully"));
        }
      
        [HttpPut("Update-Category")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryDTO categoryDTO, CancellationToken cancellationToken = default)
        {
            var command = new UpdateCategoryCommand(categoryDTO);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result)
                return NotFound(new ResponseAPI(404, $"Category with id was not found"));

            return Ok(new ResponseAPI(200, "Item has been updated successfully"));
        }

        [HttpDelete("Delete-Category/{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id),cancellationToken);

            if (!result)
                return NotFound(new ResponseAPI(404, $"Category with Id {id} not found" ));

            return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));
        }

    }
}
