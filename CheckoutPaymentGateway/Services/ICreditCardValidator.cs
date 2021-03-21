using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Services
{
    public interface ICreditCardValidator
    {
        bool IsValid(CreditCard creditCard);
    }
}
