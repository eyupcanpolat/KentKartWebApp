namespace KentKart.Api.DTOs.Payments;

public class PaymentResponseDto
{
    public int PaymentId { get; set; }

    public int CardId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal CurrentBalance { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string PaymentType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public string? Description { get; set; }
}