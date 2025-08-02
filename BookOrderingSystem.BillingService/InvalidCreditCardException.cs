namespace BookOrderingSystem.BillingService;

public class InvalidCreditCardException : Exception
{
    public InvalidCreditCardException() : base("Die Kreditkarte ist ungültig. Bitte überprüfen Sie die Kartendaten.")
    {
    }

    public InvalidCreditCardException(string message): base(message)
    {
    }

    public InvalidCreditCardException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
