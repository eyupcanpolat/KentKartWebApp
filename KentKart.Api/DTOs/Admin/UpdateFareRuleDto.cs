namespace KentKart.Api.DTOs.Admin;

public class UpdateFareRuleDto
{
    public decimal Price { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public bool IsActive { get; set; } = true;
}