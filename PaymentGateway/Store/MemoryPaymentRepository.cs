using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Payment;

namespace CheckoutPaymentGateway.Store
{
    public class MemoryPaymentRepository : IPaymentRepository
    {
        private readonly Dictionary<Guid, Payment.Payment> _payments = new Dictionary<Guid, Payment.Payment>();

        public Task<Payment.Payment> Store(Guid paymentId, MaskedPaymentRequest paymentRequest, PaymentResponse paymentResponse)
        {
            var payment = new Payment.Payment(paymentRequest, paymentResponse);

            _payments.Add(paymentId, new Payment.Payment(paymentRequest, paymentResponse));

            return Task.FromResult(payment);
        }

        public Task<Payment.Payment> Get(Guid paymentId)
        {
            return Task.FromResult(_payments[paymentId]);
        }
    }
}
