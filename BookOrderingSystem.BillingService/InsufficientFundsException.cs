namespace BookOrderingSystem.BillingService;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException() : base("Nicht genügend Guthaben für die Zahlung. Bitte laden Sie Ihr Konto auf.")
    {
    }

    public InsufficientFundsException(string message): base(message)
    {
    }

    public InsufficientFundsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
