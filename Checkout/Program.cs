using System;

namespace Checkout
{
    public class PaymentGateway : IPaymentGateway
    {
        private readonly ICreditCardValidator _creditCardValidator;
        private readonly IBank _bank;

        public PaymentGateway(ICreditCardValidator creditCardValidator, IBank bank)
        {
            _creditCardValidator = creditCardValidator;
            _bank = bank;
        }

        public PaymentResponse Submit(PaymentRequest paymentRequest)
        {
            if (_creditCardValidator.IsValid(paymentRequest.CreditCardInformation))
            {
                var bankPaymentResult = _bank.ProceedPayment(paymentRequest);

            }
        }
    }

    public interface IBank
    {
        bool ProceedPayment(PaymentRequest paymentRequest);
    }

    public interface ICreditCardValidator
    {
        bool IsValid(CreditCardInformation creditCardInformation);
    }

    public interface IPaymentGateway
    {
        PaymentResponse Submit(PaymentRequest paymentRequest);
    }

    public class PaymentResponse
    {
        public Guid PaymentId { get; set; }
        public bool Success { get; set; }
    }

    public class CreditCardInformation
    {
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public uint Cvv { get; set; }
    }

    public class PaymentRequest
    {
        public CreditCardInformation CreditCardInformation { get; set; }
        public decimal Amount { get; set; }
    }

    public class Shopper
    {
        public Guid Id { get; set; }
    }

    public class Merchant
    {
        public Guid Id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
