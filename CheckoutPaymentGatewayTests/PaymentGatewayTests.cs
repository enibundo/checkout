using System;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Bank;
using CheckoutPaymentGateway.Payment;
using CheckoutPaymentGateway.Services;
using CheckoutPaymentGateway.Store;
using Moq;
using NUnit.Framework;
using PaymentGateway = CheckoutPaymentGateway.Payment.PaymentGateway;

namespace CheckoutPaymentGatewayTests
{
    public class PaymentGatewayTests
    {
        private readonly Mock<IPaymentRequestPreProcessor> _paymentRequestPreProcessor;
        private readonly Mock<IPaymentRepository> _paymentRepository;
        private readonly Mock<IBank> _bank;
        private readonly PaymentGateway _paymentGateway;
        private readonly Mock<IPaymentIdProvider> _paymentIdProvider;

        public PaymentGatewayTests()
        {
            _paymentIdProvider = new Mock<IPaymentIdProvider>();
            _paymentRequestPreProcessor = new Mock<IPaymentRequestPreProcessor>();
            _paymentRepository = new Mock<IPaymentRepository>();
            _bank = new Mock<IBank>();

            _paymentGateway = new PaymentGateway(_paymentIdProvider.Object, 
                                                 _paymentRequestPreProcessor.Object, 
                                                 _paymentRepository.Object, 
                                                 _bank.Object);
        }


        [Test]
        public async Task should_call_bank_and_store_if_credit_card_is_valid()
        {
            // arrange
            var paymentId = Guid.NewGuid();
            var paymentRequest = new PaymentRequest();
            _paymentIdProvider.Setup(x => x.Get())
                              .Returns(paymentId);

            _paymentRequestPreProcessor.Setup(x => x.Process(It.IsAny<PaymentRequest>()))
                                       .Returns(new MaskedPaymentRequest
                                       {
                                           IsValid = true
                                       });

            // act
            await _paymentGateway.Submit(paymentRequest);

            // assert
            _bank.Verify(x => x.ProceedPayment(paymentRequest), Times.Once());
            _paymentRepository.Verify(x=>x.Store(paymentId, It.IsAny<MaskedPaymentRequest>(), It.IsAny<PaymentResponse>()), Times.Once);
        }


        [Test]
        public async Task should_not_call_bank_but_still_store_if_credit_card_is_invalid()
        {
            // arrange
            var paymentId = Guid.NewGuid();
            var paymentRequest = new PaymentRequest();
            _paymentIdProvider.Setup(x => x.Get())
                              .Returns(paymentId);

            _paymentRequestPreProcessor.Setup(x => x.Process(It.IsAny<PaymentRequest>()))
                                       .Returns(new MaskedPaymentRequest
                                       {
                                           IsValid = false
                                       });

            // act
            await _paymentGateway.Submit(paymentRequest);

            // assert
            _bank.Verify(x=>x.ProceedPayment(paymentRequest), Times.Never());
            _paymentRepository.Verify(x => x.Store(paymentId, It.IsAny<MaskedPaymentRequest>(), It.IsAny<PaymentResponse>()), Times.Once);
        }
    }
}
