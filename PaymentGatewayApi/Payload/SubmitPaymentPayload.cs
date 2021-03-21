namespace CheckoutPaymentGatewayApi.Payload
{
    public class SubmitPaymentPayload
    {
        public CreditCardInformation CreditCardInformation { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
