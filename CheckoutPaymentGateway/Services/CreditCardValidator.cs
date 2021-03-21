using System;
using System.Linq;
using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Services
{
    public class CreditCardValidator : ICreditCardValidator
    {
        public bool IsValid(CreditCard creditCard)
        {
            return IsCreditCardNumberValid(creditCard) && 
                   IsNameSet(creditCard) && 
                   IsDateNotPast(creditCard) && 
                   IsCcvValidFormat(creditCard);
        }

        private bool IsCreditCardNumberValid(CreditCard creditCard)
        {
            if (string.IsNullOrWhiteSpace(creditCard.Number))
                return false;

            var creditCardNoDashesSpaces = creditCard.Number
                                                     .Replace("' ", string.Empty)
                                                     .Replace("-", string.Empty);

            return creditCardNoDashesSpaces.Length == 16 && 
                   creditCardNoDashesSpaces.All(x => x >= '0' && x <= '9');
        }

        private bool IsDateNotPast(CreditCard creditCard)
        {
            return creditCard.ExpiryDate > DateTime.Today;
        }

        private bool IsNameSet(CreditCard creditCard)
        {
            return !string.IsNullOrWhiteSpace(creditCard.HolderFirstName) && 
                   !string.IsNullOrWhiteSpace(creditCard.HolderFirstName) && 
                   !string.IsNullOrWhiteSpace(creditCard.HolderLastName) && 
                   !string.IsNullOrWhiteSpace(creditCard.HolderLastName);
        }

        private bool IsCcvValidFormat(CreditCard creditCard)
        {
            return creditCard.Cvv.Length == 3 && creditCard.Cvv.All(x => x >= '0' && x <= '9');
        }
    }
}
