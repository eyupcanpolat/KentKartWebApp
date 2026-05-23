namespace KentKart.Api.DTOs.Admin;

public class StationResponseDto
{
    public int StationId { get; set; }

    public string StationName { get; set; } = string.Empty;

    public string? District { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}