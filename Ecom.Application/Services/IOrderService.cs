

using Ecom.Application.DTOs;
using Ecom.Domain.Entities.Order;

namespace Ecom.Core.Services
{
    public interface IOrderService
    {
        //Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string BuyerEmail);
        //Task<IReadOnlyList<OrderResponse>> GetAllOrdersForUserAsync( string BuyerEmail);

        //Task<OrderResponse> GetOrdersById(int id, string BuyerEmail);
        //Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync();
        Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string buyerEmail);
        Task<IReadOnlyList<OrderResponse>> GetAllOrdersForUserAsync(string buyerEmail);
        Task<OrderResponse> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    }
}
