using System;
using System.Linq;
using System.Text.RegularExpressions;
using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Services
{
    public class PaymentRequestPreProcessor : IPaymentRequestPreProcessor
    {
        private readonly IMaskCreditCardService _maskCreditCardService;

        public PaymentRequestPreProcessor(IMaskCreditCardService maskCreditCardService)
        {
            _maskCreditCardService = maskCreditCardService;
        }

        public bool IsValid(CreditCard creditCard)
        {
            if (string.IsNullOrEmpty(creditCard.Number))
                return false;

            creditCard.Number = SanitizeCreditCardNumber(creditCard.Number);

            return IsCreditCardNumberValid(creditCard) && 
                   IsNameSet(creditCard) && 
                   IsDateNotPast(creditCard) && 
                   IsCcvValidFormat(creditCard);
        }

        private bool IsCreditCardNumberValid(CreditCard creditCard)
        {
            if (string.IsNullOrWhiteSpace(creditCard.Number))
                return false;

            var sanitizedCreditCardNumber = SanitizeCreditCardNumber(creditCard.Number);

            return sanitizedCreditCardNumber.Length == 16;
        }

        private string SanitizeCreditCardNumber(string creditCardNumber)
        {
            var nonDigit = new Regex("[^0-9]");
            var res = nonDigit.Replace(creditCardNumber, string.Empty);
            return res;
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


        public MaskedPaymentRequest Process(PaymentRequest paymentRequest)
        {
            var isValid = IsValid(paymentRequest.CreditCard);
            return new MaskedPaymentRequest
            {
                Amount = paymentRequest.Amount,
                IsValid = isValid,
                Currency = paymentRequest.Currency,
                MaskedCreditCard = _maskCreditCardService.Mask(paymentRequest.CreditCard)
            };
        }
    }
}
