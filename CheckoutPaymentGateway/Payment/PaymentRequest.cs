namespace CheckoutPaymentGateway.Payment
{
    public class PaymentRequest
    {
        public CreditCard CreditCard { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
