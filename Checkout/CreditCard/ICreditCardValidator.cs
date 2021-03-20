namespace PaymentGateway.CreditCard
{
    public interface ICreditCardValidator
    {
        bool IsValid(CreditCardInformation creditCardInformation);
    }
}
