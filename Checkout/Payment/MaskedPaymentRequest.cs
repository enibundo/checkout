using PaymentGateway.CreditCard;

namespace PaymentGateway.Payment
{
    public class MaskedPaymentRequest
    {
        public MaskedCreditCardInformation MaskedCreditCardInformation { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
