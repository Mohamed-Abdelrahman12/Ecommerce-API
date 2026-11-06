using Ecom.Application.DTOs;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order")]

        public async Task<IActionResult> CreateOrderAsync(OrderDTO orderDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var order = await _orderService.CreateOrderAsync(orderDTO, email);

            return Ok(order);
        }
        [HttpGet("get-order-for-user")]
        public async Task<IActionResult> getorders()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var order = await _orderService.GetAllOrdersForUserAsync(email);
            return Ok(order);
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var order = await _orderService.GetOrderByIdAsync(id,email);
            if (order is null)
                return NotFound($"Order {id} not found.");

            return Ok(order);
        }

        [HttpGet("get-delivery")]
        public async Task<IActionResult> GetDelivery()
        {
            var deliveryMethods = await _orderService.GetAllDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
