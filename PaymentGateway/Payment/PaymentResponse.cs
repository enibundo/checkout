using System;

namespace PaymentGateway.Payment
{
    public class PaymentResponse
    {
        public Guid PaymentId { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public static PaymentResponse FromBankResult(Guid paymentId, bool bankPaymentResult)
        {
            return new PaymentResponse
            {
                Success = bankPaymentResult,
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
