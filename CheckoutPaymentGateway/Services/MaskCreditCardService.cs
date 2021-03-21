using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Services
{
    public class MaskCreditCardService : IMaskCreditCardService
    {
        public MaskedCreditCard Mask(CreditCard creditCard)
        {
            var lastFourDigitsOfCreditCard = creditCard.Number.Length == 16
                ? creditCard.Number[12..16]
                : "****";

            return new MaskedCreditCard
            {
                ExpiryDate = creditCard.ExpiryDate,
                HolderFirstName = creditCard.HolderFirstName,
                HolderLastName = creditCard.HolderLastName,
                CreditCardNumber = $"****-****-****-{lastFourDigitsOfCreditCard}"
            };
        }
    }
}
