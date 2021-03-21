using System;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Bank;
using CheckoutPaymentGateway.Services;
using CheckoutPaymentGateway.Store;
using Serilog;

namespace CheckoutPaymentGateway.Payment
{
    public class PaymentGateway : IPaymentGateway
    {
        private readonly IPaymentIdProvider _paymentIdProvider;
        private readonly IPaymentRequestPreProcessor _paymentRequestPreProcessor;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBank _bank;

        public PaymentGateway(IPaymentIdProvider paymentIdProvider,
                              IPaymentRequestPreProcessor paymentRequestPreProcessor,
                              IPaymentRepository paymentRepository,
                              IBank bank)
        {
            _paymentIdProvider = paymentIdProvider;
            _paymentRequestPreProcessor = paymentRequestPreProcessor;
            _paymentRepository = paymentRepository;
            _bank = bank;
        }

        public async Task<PaymentResponse> Submit(PaymentRequest paymentRequest)
        {
            var paymentId = _paymentIdProvider.Get();

            Log.Information("Submit payment {paymentId}", paymentId);

            var paymentResponse = PaymentResponse.InvalidCreditCard(paymentId);
            var maskedRequest = _paymentRequestPreProcessor.Process(paymentRequest);

            if (maskedRequest.IsValid)
            {
                var bankPaymentResult = await _bank.ProceedPayment(paymentRequest);
                paymentResponse = PaymentResponse.FromBankResult(paymentId, bankPaymentResult);
            }
            
            Log.Information("Payment {paymentId} and {paymentResponse}", paymentId, paymentResponse);

            await _paymentRepository.Store(paymentId, maskedRequest, paymentResponse);
            return paymentResponse;
        }
        
        public async Task<Payment> Get(Guid paymentId)
        {
            return await _paymentRepository.Get(paymentId);
        }
    }
}
