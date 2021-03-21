using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Services
{
    public interface IPaymentRequestPreProcessor
    {
        MaskedPaymentRequest Process(PaymentRequest paymentRequest);
    }
}
