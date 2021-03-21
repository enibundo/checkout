using System;

namespace CheckoutPaymentGateway.Services
{
    public interface IPaymentIdProvider
    {
        Guid Get();
    }
    public class PaymentIdProvider : IPaymentIdProvider
    {
        public Guid Get()
        {
            return Guid.NewGuid();
        }
    }
}
