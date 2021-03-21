using System;
using CheckoutPaymentGateway.Payment;
using CheckoutPaymentGateway.Services;
using Moq;
using NUnit.Framework;

namespace CheckoutPaymentGatewayTests
{
    public class PaymentRequestPreProcessorTests
    {
        private readonly IPaymentRequestPreProcessor paymentRequestProcessor;
        private readonly Mock<IMaskCreditCardService> _maskCreditCardService;
        public PaymentRequestPreProcessorTests()
        {
            _maskCreditCardService = new Mock<IMaskCreditCardService>();
            paymentRequestProcessor = new PaymentRequestPreProcessor(_maskCreditCardService.Object);
        }

        [Test]
        public void should_fail_with_credit_card_number_null()
        {
            // arrange
            var paymentRequest = GetPaymentRequest(1.2m, null);

            // act
            var maskedRequest = paymentRequestProcessor.Process(paymentRequest);
            
            // assert
            Assert.IsFalse(maskedRequest.IsValid);
        }

        [Test]
        public void should_fail_with_credit_card_number_empty()
        {
            // arrange
            var creditCardInformation = GetPaymentRequest(1.2m, string.Empty);

            // act
            var maskedRequest = paymentRequestProcessor.Process(creditCardInformation);

            // assert
            Assert.IsFalse(maskedRequest.IsValid);
        }

        [Test]
        public void should_fail_with_credit_card_not_16_digits()
        {
            // arrange
            var creditCardInformation = GetPaymentRequest(1.2m, "1234-4321-2222-123");

            // act
            var maskedRequest = paymentRequestProcessor.Process(creditCardInformation);

            // assert
            Assert.IsFalse(maskedRequest.IsValid);
        }

        [Test]
        public void should_fail_with_zero_amount()
        {
            // arrange
            var creditCardInformation = GetPaymentRequest(0, "1234-4321-2222-123");

            // act
            var maskedRequest = paymentRequestProcessor.Process(creditCardInformation);

            // assert
            Assert.IsFalse(maskedRequest.IsValid);
        }

        [Test]
        public void should_fail_with_negative_amount()
        {
            // arrange
            var creditCardInformation = GetPaymentRequest(0, "1234-4321-2222-123");

            // act
            var maskedRequest = paymentRequestProcessor.Process(creditCardInformation);

            // assert
            Assert.IsFalse(maskedRequest.IsValid);
        }

        [TestCase("1234-4321-2222-1234")]
        [TestCase("1234 4321 2222 1234")]
        [TestCase("1 - 2- 3 4   4321   2 2 2 2 - 1 2 3 4")]
        [TestCase("1234432122221234")]
        public void should_succeed_with_normal_case(string cardNumber)
        {
            // arrange
            var creditCardInformation = GetPaymentRequest(1.2m, cardNumber);

            // act
            var maskedRequest = paymentRequestProcessor.Process(creditCardInformation);

            // assert
            Assert.IsTrue(maskedRequest.IsValid);
        }

        private PaymentRequest GetPaymentRequest(decimal amount, string cardNumber)
        {
            return new PaymentRequest
            {
                Amount = amount,
                CreditCard =
                    new CreditCard
                    {
                        Number = cardNumber,
                        Cvv = "123",
                        HolderLastName = "Smith",
                        HolderFirstName = "John",
                        ExpiryDate = DateTime.Today.AddYears(10)
                    },
                Currency = "EUR"
            };
        }
    }
}