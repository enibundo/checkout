using PaymentGateway.CreditCard;

namespace PaymentGateway.Payment
{
    public class PaymentRequest
    {
        public CreditCardInformation CreditCardInformation { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public MaskedPaymentRequest AsMasked()
        {
            return new MaskedPaymentRequest
            {
                MaskedCreditCardInformation = new MaskedCreditCardInformation
                {
                    
                }
            };
        }
    }
}
