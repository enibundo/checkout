using System;

namespace PaymentGateway.Payment
{
    public class PaymentResponse
    {
        public Guid PaymentId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public static PaymentResponse FromBankResult(Guid paymentId, bool bankPaymentResult)
        {
            return new PaymentResponse
            {
                Success = false,
                PaymentId = paymentId,
                Message = "Bank response"
            };
        }

        public static PaymentResponse InvalidCreditCard(Guid paymentId)
        {
            return new PaymentResponse
            {
                Success = false,
                PaymentId = paymentId,
                Message = "Invalid card number"
            };
        }
    }
}
