using System;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Payment;
using CheckoutPaymentGatewayApi.Payload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckoutPaymentGatewayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentGateway _paymentGateway;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentGateway paymentGateway,
                                 ILogger<PaymentController> logger)
        {
            _paymentGateway = paymentGateway;
            _logger = logger;
        }

        [HttpGet("{paymentId}")]
        public async Task<JsonResult> Get(Guid  paymentId)
        {
            return new JsonResult(await _paymentGateway.Get(paymentId));
        }

        [HttpPost]
        public async Task<JsonResult> Submit(SubmitPaymentPayload submitPaymentPayload)
        {
            var paymentRequest = GetPaymentRequest(submitPaymentPayload);

            return new JsonResult(await _paymentGateway.Submit(paymentRequest));
        }

        private PaymentRequest GetPaymentRequest(SubmitPaymentPayload submitPaymentPayload)
        {
            return new PaymentRequest
            {
                Amount = submitPaymentPayload.Amount,
                Currency = submitPaymentPayload.Currency,
                CreditCard = submitPaymentPayload.CreditCard
            };
        }
    }
}
