using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        // IConfiguration is no longer needed here — PaymentService owns all Stripe config
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // ── Create / refresh a PaymentIntent for the cart ──────────────────────
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<CustomerCart>> Create(string cartId, int? deliveryId)
        {
            var cart = await _paymentService.CreateOrUpdateAsync(cartId, deliveryId);

            if (cart is null)
                return BadRequest("Cart not found or could not be updated.");

            return Ok(cart);
        }

        // ── Stripe webhook ──────────────────────────────────────────────────────
        // No [Authorize] — Stripe calls this, not the user.
        // No Stripe types here — all parsing/logic is in PaymentService (infrastructure layer).
        //
        // Local test mode setup:
        //   1. Install Stripe CLI → https://stripe.com/docs/stripe-cli
        //   2. stripe listen --forward-to https://localhost:{port}/api/payments/webhook
        //   3. Paste the printed whsec_... into appsettings.json → StripeSetting:WebhookSecret
        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            // Read raw body — required for Stripe signature verification
            string json;
            using (var reader = new StreamReader(HttpContext.Request.Body))
                json = await reader.ReadToEndAsync();

            try
            {
                await _paymentService.HandleWebhookAsync(
                    json,
                    Request.Headers["Stripe-Signature"]
                );

                // Always 200 — any other response causes Stripe to retry
                return Ok();
            }
            catch (StripeException ex)
            {
                // Bad signature — reject
                return BadRequest($"Webhook error: {ex.Message}");
            }
        }
    }
}