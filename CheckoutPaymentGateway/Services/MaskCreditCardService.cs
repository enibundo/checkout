using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Services
{
    public class MaskCreditCardService : IMaskCreditCardService
    {
        public MaskedCreditCard Mask(CreditCard creditCard)
        {
            var lastFourDigitsOfCreditCard = creditCard.CardNumber[12..16];

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
