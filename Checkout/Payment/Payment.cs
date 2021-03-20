namespace PaymentGateway.Payment
{
    public class Payment 
    {
        public Payment(MaskedPaymentRequest paymentRequest, PaymentResponse paymentResponse)
        {
            PaymentRequest = paymentRequest;
            PaymentResponse = paymentResponse;
        }

        public MaskedPaymentRequest PaymentRequest { get;  }
        public PaymentResponse PaymentResponse { get; }
    }
}
