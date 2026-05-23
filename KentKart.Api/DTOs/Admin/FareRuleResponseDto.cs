namespace KentKart.Api.DTOs.Admin;

public class FareRuleResponseDto
{
    public int FareRuleId { get; set; }

    public int CardTypeId { get; set; }

    public string CardTypeName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public bool IsActive { get; set; }
}