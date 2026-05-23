using KentKart.Api.DTOs.Admin;
using KentKart.Api.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("bus-lines")]
    public async Task<IActionResult> GetBusLines()
    {
        var result = await _adminService.GetBusLinesAsync();

        return Ok(result);
    }

    [HttpPost("bus-lines")]
    public async Task<IActionResult> CreateBusLine(CreateBusLineDto dto)
    {
        try
        {
            var result = await _adminService.CreateBusLineAsync(dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("bus-lines/{busLineId}")]
    public async Task<IActionResult> UpdateBusLine(int busLineId, UpdateBusLineDto dto)
    {
        try
        {
            var result = await _adminService.UpdateBusLineAsync(busLineId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("stations")]
    public async Task<IActionResult> GetStations()
    {
        var result = await _adminService.GetStationsAsync();

        return Ok(result);
    }

    [HttpPost("stations")]
    public async Task<IActionResult> CreateStation(CreateStationDto dto)
    {
        try
        {
            var result = await _adminService.CreateStationAsync(dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("stations/{stationId}")]
    public async Task<IActionResult> UpdateStation(int stationId, UpdateStationDto dto)
    {
        try
        {
            var result = await _adminService.UpdateStationAsync(stationId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("fare-rules")]
    public async Task<IActionResult> GetFareRules()
    {
        var result = await _adminService.GetFareRulesAsync();

        return Ok(result);
    }

    [HttpPost("fare-rules")]
    public async Task<IActionResult> CreateFareRule(CreateFareRuleDto dto)
    {
        try
        {
            var result = await _adminService.CreateFareRuleAsync(dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("fare-rules/{fareRuleId}")]
    public async Task<IActionResult> UpdateFareRule(int fareRuleId, UpdateFareRuleDto dto)
    {
        try
        {
            var result = await _adminService.UpdateFareRuleAsync(fareRuleId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}