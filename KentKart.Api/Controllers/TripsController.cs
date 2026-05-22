using KentKart.Api.DTOs.Trips;
using KentKart.Api.Services.Trips;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrip(CreateTripDto dto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _tripService.CreateTripAsync(userId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTrips()
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _tripService.GetMyTripsAsync(userId);

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