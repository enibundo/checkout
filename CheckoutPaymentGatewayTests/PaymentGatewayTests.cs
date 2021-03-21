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
        private readonly Mock<ICreditCardValidator> _creditCardValidator;
        private readonly Mock<IMaskCreditCardService> _maskCreditCardService;
        private readonly Mock<IPaymentRepository> _paymentRepository;
        private readonly Mock<IBank> _bank;
        private readonly PaymentGateway _paymentGateway;

        public PaymentGatewayTests()
        {
            _creditCardValidator = new Mock<ICreditCardValidator>();
            _maskCreditCardService = new Mock<IMaskCreditCardService>();
            _paymentRepository = new Mock<IPaymentRepository>();
            _bank = new Mock<IBank>();

            _paymentGateway = new PaymentGateway(_creditCardValidator.Object, _maskCreditCardService.Object, _paymentRepository.Object, _bank.Object);
        }

        [Test]
        public async Task should_not_call_bank_if_credit_card_is_invalid()
        {
            // arrange
            _creditCardValidator.Setup(x => x.IsValid(It.IsAny<CreditCard>()))
                                .Returns(false);

            // act
            await _paymentGateway.Submit(new PaymentRequest());

            // assert
            _bank.Verify(x=>x.ProceedPayment(It.IsAny<PaymentRequest>()), Times.Never);
        }
    }
}
