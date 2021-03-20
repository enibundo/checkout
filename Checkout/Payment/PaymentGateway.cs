using System;
using PaymentGateway.Bank;
using PaymentGateway.CreditCard;
using PaymentGateway.Store;

namespace PaymentGateway.Payment
{
    public class PaymentGateway : IPaymentGateway
    {
        private readonly ICreditCardValidator _creditCardValidator;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBank _bank;

        public PaymentGateway(ICreditCardValidator creditCardValidator, 
                              IPaymentRepository paymentRepository,
                              IBank bank)
        {
            _creditCardValidator = creditCardValidator;
            _paymentRepository = paymentRepository;
            _bank = bank;
        }

        public PaymentResponse Submit(PaymentRequest paymentRequest)
        {
            var paymentId = Guid.NewGuid();
         
            var paymentResponse = PaymentResponse.InvalidCreditCard(paymentId);

            if (_creditCardValidator.IsValid(paymentRequest.CreditCardInformation))
            {
                var bankPaymentResult = _bank.ProceedPayment(paymentRequest);
             
                paymentResponse = PaymentResponse.FromBankResult(paymentId, bankPaymentResult);
            }

            _paymentRepository.Store(paymentId, paymentRequest.AsMasked(), paymentResponse);

            return paymentResponse;
        }

        public Payment Get(Guid paymentId)
        {
            return _paymentRepository.Get(paymentId);
        }
    }
}
