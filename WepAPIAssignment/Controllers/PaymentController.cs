using Core.entites;
using Core.entites.OrderAggragate;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;
        private const string whSecret = "whsec_0687331d88c1cc11216346d00ae72f951b84160d2ccc058abe60c1fc3481a0ad"; 
        public PaymentController(IPaymentService _paymentService,ILogger<PaymentController> _logger)
        {
            paymentService = _paymentService;
            logger = _logger;
        }

        [HttpPost("{BasketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrdUpdateAsyncOrder(string basketId)
        {
            var order = await paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (order == null)
                return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return Ok(order);
        }

        [HttpPost("WebHook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var StripeEvent =  EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],whSecret);

            PaymentIntent intent;
            Order order;

            switch (StripeEvent.Type)
            {
                case Events.PaymentIntentPaymentFailed:
                    intent = (PaymentIntent)StripeEvent.Data.Object;

                    logger.LogError("Payment failed payment Intent Id", intent.Id);
                    order = await paymentService.UpdatePaymentFailed(intent.Id);
                    logger.LogError("Payment failed order id : ", order.id);

                    break;

                case Events.PaymentIntentSucceeded:
                    intent = (PaymentIntent)StripeEvent.Data.Object;
                    logger.LogError("Payment succeeded", intent.Id);

                    order = await paymentService.UpdatePaymentSucceded(intent.Id);
                    logger.LogError("Payment succeeded order id", order.id);

                    break;

            }

            return new EmptyResult();
        }
    }
}
