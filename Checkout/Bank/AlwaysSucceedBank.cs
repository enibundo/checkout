using PaymentGateway.Payment;

namespace PaymentGateway.Bank
{
    public class AlwaysSucceedBank : IBank
    {
        public bool ProceedPayment(PaymentRequest paymentRequest)
        {
            return true;
        }
    }
}
