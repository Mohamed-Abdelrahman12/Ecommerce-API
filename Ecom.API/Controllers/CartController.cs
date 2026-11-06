
using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CartController : BaseController
    {
        public CartController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("Get-Cart-item/{id}")]
        public async Task<IActionResult> getCartAsync(string id)
        {
            var result = await _unitOfWork.CustomerCartRepository.GetCartAsync(id);

            if (result is null)
            {
                return Ok(new CustomerCart());
            }

            return Ok(result);
        }
        [HttpPost("Update-Cart")]
        public async Task<IActionResult> AddCartAsync(CustomerCart  customerCart)
        {
            var result = await _unitOfWork.CustomerCartRepository.UpdateCartAsync(customerCart);

            return Ok(result);
        }

        [HttpDelete("Delete-Cart/{id}")]
        public async Task<IActionResult> DeleteCartAsync(string id)
        {
            var result = await _unitOfWork.CustomerCartRepository.DeleteCartAsync(id);

            return result ? Ok(new ResponseAPI(200,"Item Deleted")) : BadRequest(new ResponseAPI(400));
        }
    }
}
