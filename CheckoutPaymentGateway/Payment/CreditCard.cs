using System;

namespace CheckoutPaymentGateway.Payment
{
    public class CreditCard
    {
        public string Number { get; set; }
        public string HolderFirstName { get; set; }
        public string HolderLastName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Cvv { get; set; }
    }
}
