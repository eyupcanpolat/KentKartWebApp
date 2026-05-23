namespace KentKart.Api.DTOs.Admin;

public class CreateFareRuleDto
{
    public int CardTypeId { get; set; }

    public decimal Price { get; set; }

    public DateTime ValidFrom { get; set; } = DateTime.Now;

    public DateTime? ValidTo { get; set; }
}
