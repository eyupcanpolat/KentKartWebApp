namespace KentKart.Api.DTOs.Admin;

public class CreateStationDto
{
    public string StationName { get; set; } = string.Empty;

    public string? District { get; set; }
}