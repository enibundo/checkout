using System;
using PaymentGateway.Payment;

namespace PaymentGateway.Store
{
    public interface IPaymentRepository
    {
        Payment.Payment Store(Guid paymentId, MaskedPaymentRequest paymentRequest, PaymentResponse paymentResponse);
        Payment.Payment Get(Guid paymentId);
    }
}
