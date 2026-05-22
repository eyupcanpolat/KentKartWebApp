namespace KentKart.Api.Entities;

public class Payment
{
    public int PaymentId { get; set; }

    public int CardId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string PaymentType { get; set; } = "BalanceLoad";

    public string Status { get; set; } = "Success";

    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public string? Description { get; set; }

    public Card Card { get; set; } = null!;
}