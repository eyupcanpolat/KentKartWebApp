namespace KentKart.Api.Entities;

public class CardApplication
{
    public int CardApplicationId { get; set; }

    public int UserId { get; set; }

    public int CardTypeId { get; set; }

    public DateTime ApplicationDate { get; set; } = DateTime.Now;

    public string Status { get; set; } = "Pending";

    public string? AdminNote { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public User User { get; set; } = null!;

    public CardType CardType { get; set; } = null!;
}