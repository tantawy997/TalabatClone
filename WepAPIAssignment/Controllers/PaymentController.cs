using Core.entites;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;

        public PaymentController(IPaymentService _paymentService,ILogger<PaymentController> _logger)
        {
            paymentService = _paymentService;
            logger = _logger;
        }

        public async Task<ActionResult<CustomerBasket>> CreateOrdUpdateAsyncOrder(string basketId)
        {
            var order = await paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (order == null)
                return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return Ok(order);
        }
    }
}
