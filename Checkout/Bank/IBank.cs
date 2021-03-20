using PaymentGateway.Payment;

namespace PaymentGateway.Bank
{
    public interface IBank
    {
        bool ProceedPayment(PaymentRequest paymentRequest);
    }
}
