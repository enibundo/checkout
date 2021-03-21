using System;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Bank;
using CheckoutPaymentGateway.Services;
using CheckoutPaymentGateway.Store;

namespace CheckoutPaymentGateway.Payment
{
    public class PaymentGateway : IPaymentGateway
    {
        private readonly ICreditCardValidator _creditCardValidator;
        private readonly IMaskCreditCardService _maskCreditCardService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBank _bank;

        public PaymentGateway(ICreditCardValidator creditCardValidator,
                              IMaskCreditCardService maskCreditCardService,
                              IPaymentRepository paymentRepository,
                              IBank bank)
        {
            _creditCardValidator = creditCardValidator;
            _maskCreditCardService = maskCreditCardService;
            _paymentRepository = paymentRepository;
            _bank = bank;
        }

        public async Task<PaymentResponse> Submit(PaymentRequest paymentRequest)
        {
            var paymentId = Guid.NewGuid();
            var paymentResponse = PaymentResponse.InvalidCreditCard(paymentId);

            if (_creditCardValidator.IsValid(paymentRequest.CreditCard))
            {
                var bankPaymentResult = await _bank.ProceedPayment(paymentRequest);
                paymentResponse = PaymentResponse.FromBankResult(paymentId, bankPaymentResult);
            }

            var maskedRequest = GetMaskedRequest(paymentRequest);
            await _paymentRepository.Store(paymentId, maskedRequest, paymentResponse);

            return paymentResponse;
        }

        private MaskedPaymentRequest GetMaskedRequest(PaymentRequest paymentRequest)
        {
            var maskedCreditCard = _maskCreditCardService.Mask(paymentRequest.CreditCard);

            return new MaskedPaymentRequest
            {
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                MaskedCreditCardInformation = maskedCreditCard
            };
        }

        public async Task<Payment> Get(Guid paymentId)
        {
            return await _paymentRepository.Get(paymentId);
        }
    }
}
