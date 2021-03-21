using System;
using System.Threading.Tasks;

namespace CheckoutPaymentGateway.Payment
{
    public interface IPaymentGateway
    {
        Task<PaymentResponse> Submit(PaymentRequest paymentRequest);
        Task<Payment> Get(Guid paymentId);
    }
}
