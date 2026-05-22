using KentKart.Api.DTOs.CardApplications;
using KentKart.Api.Services.CardApplications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CardApplicationsController : ControllerBase
{
    private readonly ICardApplicationService _cardApplicationService;
    
    public CardApplicationsController(ICardApplicationService cardApplicationService)
    {
        _cardApplicationService = cardApplicationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCardApplicationDto dto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _cardApplicationService.CreateAsync(userId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyApplications()
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _cardApplicationService.GetMyApplicationsAsync(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("pending")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPendingApplications()
    {
        var result = await _cardApplicationService.GetPendingApplicationsAsync();

        return Ok(result);
    }

    [HttpPut("{applicationId}/review")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Review(int applicationId, ReviewCardApplicationDto dto)
    {
        try
        {
            var result = await _cardApplicationService.ReviewAsync(applicationId, dto);

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