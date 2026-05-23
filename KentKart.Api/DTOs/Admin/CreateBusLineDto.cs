namespace KentKart.Api.DTOs.Admin;

public class CreateBusLineDto
{
    public string LineCode { get; set; } = string.Empty;

    public string LineName { get; set; } = string.Empty;

    public string? Description { get; set; }
}