namespace KentKart.Api.DTOs.Admin;

public class UpdateStationDto
{
    public string StationName { get; set; } = string.Empty;

    public string? District { get; set; }

    public bool IsActive { get; set; } = true;
}