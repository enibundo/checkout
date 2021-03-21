using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGatewayApi.Payload
{
    public class SubmitPaymentPayload
    {
        public CreditCard CreditCard { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
