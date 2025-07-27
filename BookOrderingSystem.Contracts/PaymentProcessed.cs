namespace BookOrderingSystem.Contracts;

public record PaymentProcessed(Guid OrderId, bool Success);
