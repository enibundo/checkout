using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<JsonResult> Get(GetPaymentPayload getPaymentPayload)
        {
            return new JsonResult(await _paymentGateway.Get(getPaymentPayload.PaymentId));
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
                CreditCardInformation = submitPaymentPayload.CreditCardInformation
            };
        }
    }
}
