using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Services
{
    public interface IMaskCreditCardService
    {
        MaskedCreditCard Mask(CreditCard creditCard);
    }
}
