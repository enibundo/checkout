using System.Threading.Tasks;
using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Bank
{
    public class AlwaysSucceedBank : IBank
    {
        public Task<bool> ProceedPayment(PaymentRequest paymentRequest)
        {
            return Task.FromResult(true);
        }
    }
}
