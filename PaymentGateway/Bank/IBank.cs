using System.Threading.Tasks;
using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Bank
{
    public interface IBank
    {
        Task<bool> ProceedPayment(PaymentRequest paymentRequest);
    }
}
