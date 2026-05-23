namespace KentKart.Api.DTOs.Subscriptions;

public class CardSubscriptionResponseDto
{
    public int CardSubscriptionId { get; set; }
    
    public int CardId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public int SubscriptionPlanId { get; set; }

    public string PlanName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int RemainingRideCount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = string.Empty;
}