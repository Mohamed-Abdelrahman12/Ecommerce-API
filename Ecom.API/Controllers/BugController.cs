using AutoMapper;
using Ecom.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper ) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);

            if (category is null) return NotFound();

            return Ok(category);
            
        }
        [HttpGet("Server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);

            category.Name = " ";

            return Ok(category);

        }

        [HttpGet("bad-request/{Id}")]
        public async Task<ActionResult> GetBadRequest(int Id)
        {
            

            return Ok();

        }
        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadRequest()
        {


            return BadRequest();

        }
    }
}
