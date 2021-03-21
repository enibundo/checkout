using System;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Store
{
    public interface IPaymentRepository
    {
        Task<Payment.Payment> Store(Guid paymentId, MaskedPaymentRequest paymentRequest, PaymentResponse paymentResponse);
        Task<Payment.Payment> Get(Guid paymentId);
    }
}
