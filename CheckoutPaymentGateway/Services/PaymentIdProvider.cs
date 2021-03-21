using System;

namespace CheckoutPaymentGateway.Services
{
    public class PaymentIdProvider : IPaymentIdProvider
    {
        public Guid Get()
        {
            return Guid.NewGuid();
        }
    }
}
