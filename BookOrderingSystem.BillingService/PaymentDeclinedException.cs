namespace BookOrderingSystem.BillingService;

public class PaymentDeclinedException : Exception
{
    public PaymentDeclinedException() : base("Die Zahlung wurde abgelehnt. Bitte versuchen Sie es erneut.")
    {
    }

    public PaymentDeclinedException(string message) : base(message)
    {
    }

    public PaymentDeclinedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
