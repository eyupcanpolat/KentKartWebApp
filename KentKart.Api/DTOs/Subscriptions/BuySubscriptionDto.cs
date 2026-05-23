namespace KentKart.Api.DTOs.Subscriptions;

public class BuySubscriptionDto
{
    public int CardId { get; set; }

    public int SubscriptionPlanId { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;
}