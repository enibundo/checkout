using System;

namespace PaymentGateway.CreditCard
{
    public class CreditCardInformation
    {
        public string CardNumber { get; set; }
        public string HolderFirstName { get; set; }
        public string HolderLastName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public uint Cvv { get; set; }
    }
}
