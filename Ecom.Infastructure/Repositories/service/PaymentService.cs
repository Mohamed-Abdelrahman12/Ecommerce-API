using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Order;
using Ecom.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Ecom.infrastructure.Repositories.service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _context = context;
        }

        // ── Create or refresh a Stripe PaymentIntent ────────────────────────────
        public async Task<CustomerCart> CreateOrUpdateAsync(string cartId, int? deliveryMethodId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            var cart = await _unitOfWork.CustomerCartRepository.GetCartAsync(cartId);
            if (cart is null) return null;

            // Always sync prices from DB — never trust client-sent prices
            foreach (var item in cart.cartItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                if (product is not null)
                    item.Price = product.NewPrice;
            }

            decimal shippingPrice = 0m;
            if (deliveryMethodId.HasValue)
            {
                var delivery = await _context.DeliveryMethods
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == deliveryMethodId.Value);

                if (delivery is not null)
                    shippingPrice = delivery.Price;
            }

            long amountInCents = (long)(cart.cartItems.Sum(i => i.Quantity * i.Price) * 100)
                                 + (long)(shippingPrice * 100);

            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amountInCents,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var intent = await service.CreateAsync(createOptions);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amountInCents
                };
                await service.UpdateAsync(cart.PaymentIntentId, updateOptions);
            }

            await _unitOfWork.CustomerCartRepository.UpdateCartAsync(cart);
            return cart;
        }

        // ── Webhook handler — ALL Stripe parsing logic lives here, not in the controller ──
        public async Task HandleWebhookAsync(string json, string stripeSignatureHeader)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];
            var webhookSecret = _configuration["StripeSetting:WebhookSecret"];

            // Throws StripeException if signature is invalid — controller catches it
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                stripeSignatureHeader,
                webhookSecret,
                throwOnApiVersionMismatch: false
            );

            // Only act on PaymentIntent events
            if (stripeEvent.Data.Object is not PaymentIntent intent)
                return;

            // Use our own constants — no dependency on Stripe.Events class
            switch (stripeEvent.Type)
            {
                case StripeEventTypes.PaymentIntentSucceeded:
                    await UpdateOrderSuccess(intent.Id);
                    break;

                case StripeEventTypes.PaymentIntentPaymentFailed:
                    await UpdateOrderFaild(intent.Id);
                    break;
            }
        }

        // ── Status updaters — called by HandleWebhookAsync ──────────────────────
        public async Task<Orders> UpdateOrderSuccess(string paymentIntentId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.PaymentIntentId == paymentIntentId);

            if (order is null) return null;

            order.status = Status.PaymentReceived;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Orders> UpdateOrderFaild(string paymentIntentId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.PaymentIntentId == paymentIntentId);

            if (order is null) return null;

            order.status = Status.PaymentFaild;
            await _context.SaveChangesAsync();
            return order;
        }
    }
    public static class StripeEventTypes
    {
        public const string PaymentIntentSucceeded = "payment_intent.succeeded";
        public const string PaymentIntentPaymentFailed = "payment_intent.payment_failed";
    }
}