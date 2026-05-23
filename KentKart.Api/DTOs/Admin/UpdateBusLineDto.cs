namespace KentKart.Api.DTOs.Admin;

public class UpdateBusLineDto
{
    public string LineCode { get; set; } = string.Empty;

    public string LineName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}