using KentKart.Api.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("user-dashboard")]
    public async Task<IActionResult> GetUserDashboard()
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _reportService.GetUserDashboardAsync(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("daily-revenue")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDailyRevenueReport()
    {
        var result = await _reportService.GetDailyRevenueReportAsync();

        return Ok(result);
    }

    [HttpGet("most-used-lines")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetMostUsedBusLinesReport()
    {
        var result = await _reportService.GetMostUsedBusLinesReportAsync();

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