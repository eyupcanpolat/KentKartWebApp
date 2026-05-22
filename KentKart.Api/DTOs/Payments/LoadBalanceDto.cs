namespace KentKart.Api.DTOs.Payments;

public class LoadBalanceDto
{
    public int CardId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;
}