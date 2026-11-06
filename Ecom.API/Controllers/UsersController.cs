using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.Features.Roles.AdminstrationDTO;
using Ecom.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper = null) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("Get-All-Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _unitOfWork.usersRepository.GetAllUsers();

            if (result is null)
            {
                return BadRequest(BadRequest(new ResponseAPI(400)));
            }

            return Ok(result);
        }
        [HttpGet("Get-UserBy-Name")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            var result = await _unitOfWork.usersRepository.GetUserByName(username);

            if (result is null)
            {
                return BadRequest(BadRequest(new ResponseAPI(404, "User Not Exsist")));
            }

            return Ok(result);
        }
        [HttpGet("Get-UserBy-email")]
        public async Task<IActionResult> GetUserByEmail(string useremail)
        {
            var result = await _unitOfWork.usersRepository.GetUserByEmail(useremail);

            if (result is null)
            {
                return BadRequest(BadRequest(new ResponseAPI(404, "User Not Exsist")));
            }

            return Ok(result);
        }
        [HttpGet("Get-UserWith-Roles/{userId}")]
        public async Task<IActionResult> GetUserWithRoles(string userId)
        {
            var result = await _unitOfWork.usersRepository.GetUserWithRoles(userId);

            if (result is null)
            {
                return BadRequest(BadRequest(new ResponseAPI(404, "User Not Exsist")));
            }

            return Ok(result);
        }
        [HttpPost("Add-User-Roles")]
        public async Task<IActionResult> AddUserToRolesAsync(UsersRolesDto usersRolesDto)
        {
            var result = await _unitOfWork.usersRepository.AddUserRoles(usersRolesDto);

            if (usersRolesDto is null)
            {
                return BadRequest(BadRequest(new ResponseAPI(404, "User Not Exsist")));
            }

            return Ok(new ResponseAPI(200, "Add Roles Successfully"));
        }

    }
}
