namespace KentKart.Api.DTOs.Cards;

public class CardResponseDto
{
    public int CardId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public int CardTypeId { get; set; }

    public string CardTypeName { get; set; } = string.Empty;

    public decimal DiscountRate { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}