namespace CheckoutPaymentGateway.Payment
{
    public class MaskedPaymentRequest
    {
        public MaskedCreditCard MaskedCreditCardInformation { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
