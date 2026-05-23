namespace KentKart.Api.DTOs.Subscriptions;

public class SubscriptionPlanResponseDto
{
    public int SubscriptionPlanId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int CardTypeId { get; set; }

    public string CardTypeName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int RideCount { get; set; }

    public int ValidityDays { get; set; }

    public bool IsActive { get; set; }
}