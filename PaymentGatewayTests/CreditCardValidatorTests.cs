using System;
using CheckoutPaymentGateway.Payment;
using CheckoutPaymentGateway.Services;
using NUnit.Framework;

namespace CheckoutPaymentGatewayTests
{
    public class CreditCardValidatorTests
    {
        private readonly CreditCardValidator _creditCardValidator;

        public CreditCardValidatorTests()
        {
            _creditCardValidator = new CreditCardValidator();
        }

        [Test]
        public void should_fail_with_credit_card_number_null()
        {
            // arrange
            var creditCardInformation = GetCreditCard(null);

            // act
            var isValid = _creditCardValidator.IsValid(creditCardInformation);
            
            // assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void should_fail_with_credit_card_number_empty()
        {
            // arrange
            var creditCardInformation = GetCreditCard(string.Empty);

            // act
            var isValid = _creditCardValidator.IsValid(creditCardInformation);

            // assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void should_fail_with_credit_card_not_16_digits()
        {
            // arrange
            var creditCardInformation = GetCreditCard("1234-4321-2222-123");

            // act
            var isValid = _creditCardValidator.IsValid(creditCardInformation);

            // assert
            Assert.IsFalse(isValid);
        }

        [TestCase("1234-4321-2222-1234")]
        [TestCase("1234 4321 2222 1234")]
        [TestCase("1 - 2- 3 4   4321   2 2 2 2 - 1 2 3 4")]
        [TestCase("1234432122221234")]
        public void should_succeed_with_normal_case(string cardNumber)
        {
            // arrange
            var creditCardInformation = GetCreditCard("1234-4321-2222-1234");

            // act
            var isValid = _creditCardValidator.IsValid(creditCardInformation);

            // assert
            Assert.IsTrue(isValid);
        }

        private CreditCard GetCreditCard(string cardNumber)
        {
            return new CreditCard
            {
                CardNumber = cardNumber,
                Cvv = "123",
                HolderLastName = "Smith",
                HolderFirstName = "John",
                ExpiryDate = DateTime.Today.AddYears(10)
            };
        }
    }
}