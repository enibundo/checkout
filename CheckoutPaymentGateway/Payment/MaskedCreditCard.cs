using System;

namespace CheckoutPaymentGateway.Payment
{
    public class MaskedCreditCard
    {
        public string CreditCardNumber { get; set; }
        public string HolderFirstName { get; set; }
        public string HolderLastName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Cvv => "***";
    }
}
