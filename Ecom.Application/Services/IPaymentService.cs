
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Order;


namespace Ecom.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerCart> CreateOrUpdateAsync(string CartId, int? deliveryMethodId);
        Task<Orders> UpdateOrderSuccess(string PaymentInten);
        Task<Orders> UpdateOrderFaild(string PaymentInten);
        Task HandleWebhookAsync(string json, string stripeSignatureHeader);
    }
}
