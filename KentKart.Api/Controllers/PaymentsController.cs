using KentKart.Api.DTOs.Payments;
using KentKart.Api.Services.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("load-balance")]
    public async Task<IActionResult> LoadBalance(LoadBalanceDto dto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _paymentService.LoadBalanceAsync(userId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyPayments()
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _paymentService.GetMyPaymentsAsync(userId);

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