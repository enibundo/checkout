using System;
using System.Collections.Generic;
using System.Text;
using PaymentGateway.Payment;

namespace PaymentGateway.Store
{
    public class InMemoryPaymentRepository : IPaymentRepository
    {
        private readonly Dictionary<Guid, Payment.Payment> _payments = new Dictionary<Guid, Payment.Payment>();

        public Payment.Payment Store(Guid paymentId, MaskedPaymentRequest paymentRequest, PaymentResponse paymentResponse)
        {
            var payment = new Payment.Payment(paymentRequest, paymentResponse);

            _payments.Add(paymentId, new Payment.Payment(paymentRequest, paymentResponse));

            return payment;
        }

        public Payment.Payment Get(Guid paymentId)
        {
            return _payments[paymentId];
        }
    }
}
