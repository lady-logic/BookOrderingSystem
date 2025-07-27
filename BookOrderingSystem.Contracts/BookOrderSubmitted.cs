namespace BookOrderingSystem.Contracts;

public record BookOrderSubmitted(Guid OrderId, string BookTitle, decimal Price);
