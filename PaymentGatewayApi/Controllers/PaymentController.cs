using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Payment;

namespace PaymentGatewayApi.Controllers
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
        public Payment Get(Guid paymentId)
        {
            return _paymentGateway.Get(paymentId);
        }
    }
}
