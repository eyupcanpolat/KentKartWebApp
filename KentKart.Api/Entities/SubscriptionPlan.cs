namespace KentKart.Api.Entities;

public class SubscriptionPlan
{
    public int SubscriptionPlanId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int CardTypeId { get; set; }

    public decimal Price { get; set; }

    public int RideCount { get; set; }

    public int ValidityDays { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public CardType CardType { get; set; } = null!;

    public ICollection<CardSubscription> CardSubscriptions { get; set; } = new List<CardSubscription>();
}