namespace CheckoutPaymentGateway.Payment
{
    public class MaskedPaymentRequest
    {
        public bool IsValid { get; set; }
        public MaskedCreditCard MaskedCreditCard { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
