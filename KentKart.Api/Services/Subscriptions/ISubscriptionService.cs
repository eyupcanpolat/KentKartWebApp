using KentKart.Api.DTOs.Subscriptions;

namespace KentKart.Api.Services.Subscriptions;

public interface ISubscriptionService
{
    Task<List<SubscriptionPlanResponseDto>> GetPlansAsync();

    Task<CardSubscriptionResponseDto> BuySubscriptionAsync(int userId, BuySubscriptionDto dto);

    Task<List<CardSubscriptionResponseDto>> GetMySubscriptionsAsync(int userId);
}
