using KentKart.Api.DTOs.Subscriptions;
using KentKart.Api.Services.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("plans")]
    public async Task<IActionResult> GetPlans()
    {
        var result = await _subscriptionService.GetPlansAsync();

        return Ok(result);
    }

    [HttpPost("buy")]
    public async Task<IActionResult> BuySubscription(BuySubscriptionDto dto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _subscriptionService.BuySubscriptionAsync(userId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMySubscriptions()
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _subscriptionService.GetMySubscriptionsAsync(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new Exception("Token içinde kullanıcı bilgisi bulunamadı.");
        }

        return Convert.ToInt32(userIdClaim);
    }
}