using System;

namespace PaymentGateway.Payment
{
    public interface IPaymentGateway
    {
        PaymentResponse Submit(PaymentRequest paymentRequest);
        Payment Get(Guid paymentId);
    }
}
