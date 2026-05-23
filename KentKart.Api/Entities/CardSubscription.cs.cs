namespace KentKart.Api.Entities;

public class CardSubscription
{
    public int CardSubscriptionId { get; set; }

    public int CardId { get; set; }

    public int SubscriptionPlanId { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; }

    public int RemainingRideCount { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Card Card { get; set; } = null!;

    public SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}