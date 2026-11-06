using AutoMapper;
using Ecom.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseController(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
