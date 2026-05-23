using KentKart.Api.DTOs.LostCardReports;
using KentKart.Api.Services.LostCardReports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LostCardReportsController : ControllerBase
{
    private readonly ILostCardReportService _lostCardReportService;

    public LostCardReportsController(ILostCardReportService lostCardReportService)
    {
        _lostCardReportService = lostCardReportService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLostCardReportDto dto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _lostCardReportService.CreateAsync(userId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyReports()
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _lostCardReportService.GetMyReportsAsync(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("admin/all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllReports()
    {
        var result = await _lostCardReportService.GetAllReportsAsync();

        return Ok(result);
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