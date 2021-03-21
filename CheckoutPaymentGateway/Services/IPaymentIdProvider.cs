using System;

namespace CheckoutPaymentGateway.Services
{
    public interface IPaymentIdProvider
    {
        Guid Get();
    }
}
