namespace PaymentGateway.CreditCard
{
    public class AlwaysValidCreditCardValidator : ICreditCardValidator
    {
        public bool IsValid(CreditCardInformation creditCardInformation)
        {
            return true;
        }
    }
}
